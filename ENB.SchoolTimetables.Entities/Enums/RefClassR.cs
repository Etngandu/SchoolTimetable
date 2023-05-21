using System.ComponentModel.DataAnnotations;

namespace ENB.SchoolTimetables.Entities
{
    /// <summary>
    /// Determines the type of a contact record.
    /// </summary>
    public enum RefClassR
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        [Display(Name = "1er Primaire A")]
        /// <summary>
        /// Indicates a Male Guest.
        /// </summary>        
        pre_primaire_A = 1,

        [Display(Name = "1er Primaire B")]
        /// <summary>
        /// Indicates a Female Guest.
        /// </summary>        
        pre_primaire_B = 2,

        [Display(Name = "2eme Primaire A")]
        /// <summary>
        /// Indicates a Male Guest.
        /// </summary>        
        deu_primaire_A = 3,

        [Display(Name = "2eme Primaire B")]
        /// <summary>
        /// Indicates a Female Guest.
        /// </summary>        
        deu_primaire_B = 4,

        [Display(Name = "3eme Primaire A")]
        /// <summary>
        /// Indicates a Male Guest.
        /// </summary>        
        tr_primaire_A = 5,

        [Display(Name = "3eme Primaire B")]
        /// <summary>
        /// Indicates a Female Guest.
        /// </summary>        
        tr_primaire_B = 6,

        [Display(Name = "4eme Primaire A")]
        /// <summary>
        /// Indicates a Male Guest.
        /// </summary>        
        qtr_primaire_A = 7,

        [Display(Name = "4eme Primaire B")]
        /// <summary>
        /// Indicates a Female Guest.
        /// </summary>        
        qtr_primaire_B = 8,

        [Display(Name = "5eme Primaire A")]
        /// <summary>
        /// Indicates a Male Guest.
        /// </summary>        
        cq_primaire_A = 9,

        [Display(Name = "5eme Primaire B")]
        /// <summary>
        /// Indicates a Female Guest.
        /// </summary>        
        cq_primaire_B = 10,

        [Display(Name = "6eme Primaire A")]
        /// <summary>
        /// Indicates a Male Guest.
        /// </summary>        
        six_primaire_A = 11,

        [Display(Name = "6eme Primaire B")]
        /// <summary>
        /// Indicates a Female Guest.
        /// </summary>        
        six_primaire_B = 12


    }
}
