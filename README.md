# Kogane Text Ids Code Generator

TextId を管理するクラスのコードを生成するエディタ拡張

## 使用例

```csharp
using Kogane;
using UnityEditor;

public static class Example
{
    [MenuItem( "Tools/Hoge" )]
    private static void Hoge()
    {
        var option = new TextIdsCodeGeneratorOption
        (
            namespaceName: "MyProject",
            classComment: "TextId を管理するクラス",
            className: "TextIds",
            countComment: "TextId の数を返します",
            allComment: "すべての TextId を返します",
            values: new TextIdsCodeGeneratorOption.Value[]
            {
                new
                (
                    name: "NORMAL",
                    comment: "ノーマル",
                    key: "NORMAL"
                ),
                new
                (
                    name: "FIRE",
                    comment: "ほのお",
                    key: "FIRE"
                ),
            }
        );

        TextIdsCodeGenerator.GenerateAndWrite( "Assets/TextIds.cs", option );
    }
}
```

```csharp
using System.Collections.Generic;
using Kogane;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable InconsistentNaming

namespace MyProject
{
    /// <summary>
    /// TextId を管理するクラス
    /// </summary>
    public static partial class TextIds
    {
        /// <summary>
        /// <para>ノーマル</para>
        /// </summary>
        public static TextId NORMAL { get; } = new( "NORMAL" );

        /// <summary>
        /// <para>ほのお</para>
        /// </summary>
        public static TextId FIRE { get; } = new( "FIRE" );

        /// <summary>
        /// TextId の数を返します
        /// </summary>
        public static int Count => 2;

        /// <summary>
        /// すべての TextId を返します
        /// </summary>
        public static IEnumerable<TextId> All
        {
            get
            {
                yield return NORMAL;
                yield return FIRE;
            }
        }
    }
}
```