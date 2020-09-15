//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MalekServer3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblProduct()
        {
            this.TblClientProductRels = new HashSet<TblClientProductRel>();
            this.TblProductCommentRels = new HashSet<TblProductCommentRel>();
            this.TblProductImageRels = new HashSet<TblProductImageRel>();
            this.TblProductKeywordRels = new HashSet<TblProductKeywordRel>();
            this.TblProductPropertyRels = new HashSet<TblProductPropertyRel>();
        }
    
        public int id { get; set; }
        public string Name { get; set; }
        public string DateSubmited { get; set; }
        public int Raiting { get; set; }
        public long Price { get; set; }
        public string DescriptionHtml { get; set; }
        public Nullable<int> CatagoryId { get; set; }
        public int Count { get; set; }
        public bool IsSuggested { get; set; }
        public int Discount { get; set; }
        public bool IsSlide { get; set; }
    
        public virtual TblCatagory TblCatagory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblClientProductRel> TblClientProductRels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblProductCommentRel> TblProductCommentRels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblProductImageRel> TblProductImageRels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblProductKeywordRel> TblProductKeywordRels { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblProductPropertyRel> TblProductPropertyRels { get; set; }
    }
}
