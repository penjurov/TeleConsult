namespace TeleConsult.Data.Models.Enumerations
{
    using System.ComponentModel;
    using TeleConsult.Common;

    public enum Title
    {
        [Description(GlobalConstants.Professor)]
        Professor,
        [Description(GlobalConstants.AssociateProfessor)]
        AssociateProfessor,
        [Description(GlobalConstants.AssitantProfessor)]
        AssistantProfesor,
        [Description(GlobalConstants.Md)]
        Md
    }
}
