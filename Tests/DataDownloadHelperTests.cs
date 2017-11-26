using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilePhonesOntology.Helpers;
using MobilePhonesOntology.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class DataDownloadHelperTests
    {
        [TestMethod]
        public void GetAllPhones_Success()
        {
            var phones = DataDownloadHelper.GetAllPhones().ToList();

            Assert.IsNotNull(phones);
            Assert.IsTrue(phones.Any());
            Assert.IsFalse(phones.Any(phone => string.IsNullOrEmpty(phone.Brand)));
            Assert.IsFalse(phones.Any(phone => string.IsNullOrEmpty(phone.DeviceName)));
        }

        [TestMethod]
        public async Task GetPhone_Success()
        {
            var phone = await DataDownloadHelper.GetPhone("Samsung SGH-250", "Samsung");

            Assert.IsNotNull(phone);
            Assert.AreEqual(phone.Brand, "Samsung");
            Assert.AreEqual(phone.DeviceName, "Samsung SGH-250");
        }

        [TestMethod]
        public void GetAllBrands_Success()
        {
            var brands = DataDownloadHelper.GetAllBrands().ToList();

            Assert.IsNotNull(brands);
            Assert.IsTrue(brands.Any());
            Assert.IsFalse(brands.Any(b => string.IsNullOrEmpty(b.Name)));
            Assert.IsFalse(brands.Any(b => string.IsNullOrEmpty(b.Url)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPhonesByBrandNull_Fail()
        {
            DataDownloadHelper.GetPhonesByBrand(new Brand());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPhonesByBrandEmpty_Fail()
        {
            var brand = new Brand { Url = string.Empty };
            DataDownloadHelper.GetPhonesByBrand(brand);
        }

        [TestMethod]
        public void GetPhonesByBrand_Success()
        {
            var brand = new Brand
            {
                Id = 51,
                Name = "Acer",
                Url = "https://www.phonegg.com/brand/51-Acer"
            };

            var phones = DataDownloadHelper.GetPhonesByBrand(brand).ToList();
            Assert.IsNotNull(phones);
            Assert.IsTrue(phones.Any());
            Assert.IsFalse(phones.Any(p => string.IsNullOrEmpty(p.Brand)));
            Assert.IsFalse(phones.Any(p => string.IsNullOrEmpty(p.Model)));
        }

        [TestMethod]
        public void GetAllSimplePhones_Success()
        {
            var names = DataDownloadHelper.GetAllSimplePhones();

            Assert.IsNotNull(names);
            Assert.IsTrue(names.Any());
        }

        [TestMethod]
        public async Task GetApiToken_Success()
        {
            var token = await DataDownloadHelper.GetNewApiToken();

            Assert.IsNotNull(token);
            Assert.IsFalse(string.IsNullOrEmpty(token));

            var tokenNew = await DataDownloadHelper.GetNewApiToken();
            Assert.AreEqual(token, tokenNew);
        }
    }
}