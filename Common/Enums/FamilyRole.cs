using System.ComponentModel.DataAnnotations;

namespace Common.Enums
{
    public enum FamilyRole
    {
        Me = 0,
        [Display(Name ="همسر")]
        Wife = 1,
        [Display(Name = "دختر")]
        Doughter = 2,
        [Display(Name = "پسر")]
        Son = 3,
        [Display(Name = "مادر")]
        Mother = 4,
        [Display(Name = "پدر")]
        Father = 5
    }
}