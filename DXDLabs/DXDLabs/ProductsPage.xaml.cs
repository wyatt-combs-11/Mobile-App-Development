using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;

namespace DXDLabs
{
    public partial class ProductsPage : ContentPage
    {
        public string[] ingredients = new string[] { "Agmatine Sulfate", "Citrulline Malate", "Potassium",
                                                     "Dextrose", "Vitamin C", "L-Norvaline", "Sodium" };
        public string[] ingredientInfo = new string[] {
            "Powerful nootropic which improves brain performance. It has been studied significantly and found to improve " +
            "insulin sensitivity which may help with fat loss. It also improves mood and focus, along with regulating nitric " +
            "oxide levels.",
            "Potent vasodilator which acts by raising nitric oxide levels. Nitric oxide relaxes blood vessels, dilating " +
            "them and maximizing blood flow. This, in turn, promotes nutrient delivery and increases power and endurance. " +
            "It also reduces lactic acid build-up and muscle soreness, leading to accelerated recovery.",
            "An essential electrolyte that is important for nerve impulses. Potassium is an important regulator of cardiovascular " +
            "activity, specifically, pulse rate and pulse strength.",
            "Chemically identical to glucose, which is blood sugar. It provides cellular energy and elevates energy for the body as " +
            "a whole. Dextrose makes the solution isotonic, resulting in fast absorption of the product and quicker hydration.",
            "Shown to reduce nitrate tolerance. It is a potent antioxidant, meaning that it prevents damage to cells caused by free " +
            "radicals, and is also a powerful vasodilator. It is well-known that Vitamin C boosts the immune system.",
            "Basically, it removes the limit the body places on the magnitude of the pump by inhibiting the arginase enzyme.",
            "Essential electrolyte from two sources: sodium chloride (NaCl, AKA table salt) and sodium bicarbonate (NaHCO3," +
            " AKA baking soda). These allow for enhanced hydration."
        };
        public ProductsPage()
        {
            InitializeComponent();
        }

        private void IngredientClicked(Object sender, EventArgs e)
        {
            Button b = (Button)sender;
            for (int i = 0; i < 7; i++)
            {
                if (b.Text.Equals(ingredients[i]))
                    DisplayAlert(b.Text, ingredientInfo[i], "Return to Product");
            }
        }

        private void ToggleIngredientClicked(Object sender, EventArgs e)
        {
            foreach (Button b in new Button[] { AgmatineSulfate, CitrullineMalate, Potassium, Dextrose, VitaminC, LNorvaline, Sodium })
            {
                b.FadeTo(b.Opacity == 0 ? 0.7 : 0);
            }
        }
    }
}
