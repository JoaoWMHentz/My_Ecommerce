using NUnit.Framework.Internal;
using OpenEcommerceDLL.product;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSaveProduct()
        {
            Product p = new Product();
            p.Name = "Test One";
            p.ProductId = 0;
            p.Description = "Product on Run Tests!";
            p.Category.CategoryId = 1;
            p.Brand.BrandId = 1;
            ProductImage i = new ProductImage();
            i.imageId = 0;
            i.Image = "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";
            p.SalesPerson.UserId = "Tester";
            p.Images.Add(i);
            ProductDao d = new ProductDao();
            p = d.Save(p);
            p.Name = "Product Test Update";
            p = d.Save(p);
            Assert.Pass();
        }
        [Test]
        public void TestGetProduct()
        {
            ProductDao d = new ProductDao();
            Product p = d.GetProduct(1);
            if (p.ProductId != 0)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Fail to get product");
            }

        }
        [Test]
        public void TestSaveCategory()
        {
            ProductCategory c = new ProductCategory();
            c.CategoryId = 0;
            c.Name = "Teste";
            c.Description = "Teste";
            c.Image = "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";
            c = c.salvar(c);
            c.Name = "Teste Update";
            c.salvar(c);
            Assert.Pass();
        }
        [Test]
        public void TestGetCategory()
        {
            ProductCategory c = new ProductCategory();
            c.CategoryId = 1;
            c.GetCategories(c);
            Assert.Pass();
        }
    }
}