using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace MalekServer3.Models.ObjectClass
{
    public class OcProduct : TblProduct
    {

        public List<TblImage> images { get; set; }
        public List<TblKeyword> Keyword { get; set; }
        public string AllKeywords { get; set; }
        public List<TblProperty> Properties { get; set; }
        public List<TblComment> Comments { get; set; }
        public List<TblCatagory> AllCatagorys { get; set; }
        public TblCatagory Catagory { get; set; }
        public double AfterDiscount { get; set; }
        public long Sum { get; set; }

        public OcProduct()
        {

        }
        public OcProduct(TblProduct tblProduct)
        {
            Heart heart = new Heart();
            id = tblProduct.id;
            Name = tblProduct.Name;
            DateSubmited = tblProduct.DateSubmited;
            Raiting = tblProduct.Raiting;
            Price = tblProduct.Price;
            DescriptionHtml = tblProduct.DescriptionHtml;
            CatagoryId = tblProduct.CatagoryId;
            Count = tblProduct.Count;
            Discount = tblProduct.Discount;
            IsSuggested = tblProduct.IsSuggested;
            
            List<TblImage> imagesForServeice = new List<TblImage>();
            List<TblProductImageRel> relsForImages = heart.TblProductImageRels.Where(i => i.ProductId == tblProduct.id).ToList();
            foreach (var j in relsForImages)
                imagesForServeice.Add(heart.TblImages.SingleOrDefault(i => i.id == j.ImageId));
            images = imagesForServeice;
            List<TblKeyword> keywordsForServeice = new List<TblKeyword>();
            List<TblProductKeywordRel> relsForKeys = heart.TblProductKeywordRels.Where(i => i.ProductId == tblProduct.id).ToList();
            foreach (var j in relsForKeys)
                keywordsForServeice.Add(heart.TblKeywords.SingleOrDefault(i => i.id == j.KeywordId));
            Keyword = keywordsForServeice;
            Catagory = heart.TblCatagories.SingleOrDefault(i => i.id == tblProduct.CatagoryId);
            AllCatagorys = heart.TblCatagories.ToList();
            
            List<TblProperty> propsForServeice = new List<TblProperty>();
            List<TblProductPropertyRel> relsForPros = heart.TblProductPropertyRels.Where(i => i.ProductId == tblProduct.id).ToList();
            foreach (var j in relsForPros)
                propsForServeice.Add(heart.TblProperties.SingleOrDefault(i => i.id == j.PropertyId));
            Properties = propsForServeice;
            List<TblComment> commentsForServeice = new List<TblComment>();
            List<TblProductCommentRel> relsForComments = heart.TblProductCommentRels.Where(i => i.ProductId == tblProduct.id).ToList();
            foreach (var j in relsForComments)
                commentsForServeice.Add(heart.TblComments.SingleOrDefault(i => i.id == j.CommentId));
            Comments = commentsForServeice;
            AfterDiscount = Math.Floor(Price - (double)((double)(Price * Discount) / 100));
            AllKeywords = "";

            foreach (TblKeyword Key in Keyword)
            {
                AllKeywords += Key.Name + "،";
            }
        }
    }
}