/*
/*
 * Explorus-B
 * Étienne Desbiens dese2913
 * Emily Nguyen ngub3302
 * Victoria Pitz-Clairoux pitv4001
 * Alex Chorel-Campanozzi choa3403
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Explorus;
using System.Drawing;
using System.Windows.Forms;
using Explorus.Model;
/*
namespace ExplorusTests

{
    [TestClass]
    public class HeaderTest
    {
        private Header header = Header.GetInstance();

        [TestMethod]
        public void Dimensions()
        {
            Assert.AreEqual(header.getHeaderImage().Size, new Size(1152, 96));
        }

        [TestMethod]
        public void SetLife()
        {
            header.setLife(100);
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(325, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(350, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(375, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(400, 10), Color.FromArgb(255, 114, 0, 0));
            header.setLife(55);
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(325, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(350, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(375, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(400, 10), Color.FromArgb(255, 114, 0, 0));
            header.setLife(40);
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(325, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(350, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(375, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(400, 10), Color.FromArgb(255, 114, 0, 0));
            header.setLife(20);
            Assert.AreEqual(((Bitmap)header.getHeaderImage()).GetPixel(325, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(350, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(375, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(400, 10), Color.FromArgb(255, 114, 0, 0));
            header.setLife(0);
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(325, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(350, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(375, 10), Color.FromArgb(255, 114, 0, 0));
            Assert.AreNotEqual(((Bitmap)header.getHeaderImage()).GetPixel(400, 10), Color.FromArgb(255, 114, 0, 0));
        }
    }
}*/
