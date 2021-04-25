using System.ComponentModel;

namespace CommandLineProject
{
    public enum CommandLineEnum
    {
        [Description("ADD")]
        ADD,

        [Description("MEMBERS")]
        MEMBERS,

        [Description("ALLMEMBERS")]
        ALLMEMBERS,

        [Description("CLEAR")]
        CLEAR,

        [Description("KEYS")]
        KEYS,

        [Description("KEYEXISTS")]
        KEYEXISTS,

        [Description("REMOVE")]
        REMOVE,

        [Description("REMOVEALL")]
        REMOVEALL,

        [Description("VALUEEXISTS")]
        VALUEEXISTS,

        [Description("ITEMS")]
        ITEMS
    }
}