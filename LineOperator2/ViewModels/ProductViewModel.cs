using LineOperator2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LineOperator2.ViewModels
{
    class ProductViewModel : BaseViewModel
    {
        private Product product;

        public ProductViewModel(Product sourceProduct)
        {
            product = sourceProduct;
        }

        public void SetProduct(Product newProduct)
        {
            product = newProduct;
        }


        public string PartName
        {
            get { return product.PartName; }
            set
            {
                product.PartName = value;
                NotifyPropertyChange();
            }
        }


        public int PartsPerBox
        {
            get { return product.PartsPerBox; }
            set
            {
                product.PartsPerBox = value;
                NotifyPropertyChange();
            }
        }


        public float CutLength
        {
            get { return product.CutLength; }
            set
            {
                product.CutLength = value;
                NotifyPropertyChange();
            }
        }

    }
}
