using System.Collections.Generic;

namespace Kogane
{
    public readonly struct TextIdsCodeGeneratorOption
    {
        public readonly struct Value
        {
            public string Name    { get; }
            public string Comment { get; }
            public string Key     { get; }

            public Value
            (
                string name,
                string comment,
                string key
            )
            {
                Name    = name;
                Comment = comment;
                Key     = key;
            }
        }

        public string               NamespaceName { get; }
        public string               ClassComment  { get; }
        public string               ClassName     { get; }
        public string               CountComment  { get; }
        public string               AllComment    { get; }
        public IReadOnlyList<Value> Values        { get; }

        public TextIdsCodeGeneratorOption
        (
            string               namespaceName,
            string               classComment,
            string               className,
            string               countComment,
            string               allComment,
            IReadOnlyList<Value> values
        )
        {
            NamespaceName = namespaceName;
            ClassComment  = classComment;
            ClassName     = className;
            CountComment  = countComment;
            AllComment    = allComment;
            Values        = values;
        }
    }
}