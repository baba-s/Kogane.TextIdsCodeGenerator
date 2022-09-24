using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Kogane
{
    public static class TextIdsCodeGenerator
    {
        public static void GenerateAndWrite( string path, in TextIdsCodeGeneratorOption option )
        {
            var code = Generate( option );
            Write( path, code );
        }

        public static string Generate( in TextIdsCodeGeneratorOption option )
        {
            var sourceTemplate   = LoadTemplate( "3e4baaa347f04d1bb2a5e8ee5b430490" );
            var propertyTemplate = LoadTemplate( "7678cadf038048a381948f37d5475513" );

            var values = option.Values;

            var propertyCode = values
                    .Select
                    (
                        x => propertyTemplate
                            .Replace( "#PROPERTY_COMMENT#", x.Comment.Split( "\n" ).Select( y => $"        /// <para>{y}</para>" ).ConcatWith( "\n" ) )
                            .Replace( "#PROPERTY_NAME#", x.Name )
                            .Replace( "#PROPERTY_VALUE#", x.Key )
                    )
                    .ConcatWith( "\n\n" )
                ;

            var allCode = values
                    .Select( x => $"                yield return {x.Name};" )
                    .ConcatWith( "\n" )
                ;

            const string newLineTag = "#NEW_LINE#";

            var code = sourceTemplate
                    .Replace( "#NAMESPACE_NAME#", option.NamespaceName )
                    .Replace( "#CLASS_COMMENT#", option.ClassComment )
                    .Replace( "#CLASS_NAME#", option.ClassName )
                    .Replace( "#PROPERTY_CODE#", propertyCode )
                    .Replace( "#COUNT_COMMENT#", option.CountComment )
                    .Replace( "#COUNT#", values.Count.ToString() )
                    .Replace( "#ALL_COMMENT#", option.AllComment )
                    .Replace( "#ALL_CODE#", allCode )
                    .Replace( "\t", "    " )
                    .Replace( "\r\n", newLineTag )
                    .Replace( "\r", newLineTag )
                    .Replace( "\n", newLineTag )
                    .Replace( newLineTag, "\n" )
                ;

            return code;
        }

        public static void Write( string path, string code )
        {
            var directoryName = Path.GetDirectoryName( path );

            Directory.CreateDirectory( directoryName );
            File.WriteAllText( path, code, Encoding.UTF8 );
            AssetDatabase.Refresh();
        }

        private static string LoadTemplate( string guid )
        {
            var assetPath = AssetDatabase.GUIDToAssetPath( guid );
            var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>( assetPath );

            return textAsset.text;
        }

        private static string ConcatWith( this IEnumerable<string> self, string separator )
        {
            return string.Join( separator, self );
        }
    }
}