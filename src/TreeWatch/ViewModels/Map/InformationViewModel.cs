// <copyright file="InformationViewModel.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Xamarin.Forms;

    /// <summary>
    /// Information view model.
    /// </summary>
    public class InformationViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.InformationViewModel"/> class.
        /// </summary>
        /// <param name="field">The field which information should be shown.</param>
        /// <param name="block">The block which information should be shown.</param>
        public InformationViewModel(Field field, Block block)
        {
            Field = field;
            Block = block;

            if (Field != null)
            {
                this.VarietyGroups = new ObservableCollection<Varieties>();

                Field.Blocks.Sort();
                Varieties varieties = null;
                foreach (var item in Field.Blocks)
                {
                    if ((this.VarietyGroups.Count == 0 && varieties == null) || string.Compare(varieties.Variety, item.TreeType.Name, StringComparison.Ordinal) != 0)
                    {
                        varieties = new Varieties(item.TreeType.Name, item.TreeType.ID.ToString(), item.TreeType.ColorProp);

                        varieties.Add(item);

                        this.VarietyGroups.Add(varieties);
                    }
                    else if (this.VarietyGroups.Contains(varieties))
                    {
                        varieties.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.InformationViewModel"/> class.
        /// </summary>
        /// <param name="field">The field which information should be shown.</param>
        public InformationViewModel(Field field)
            : this(field, null)
        {
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the size of the field.
        /// </summary>
        /// <value>The size of the field.</value>
        public static string FieldSize
        {
            get
            {
                return "0 m";
            }
        }

        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>The field.</value>
        public Field Field
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the variety groups.
        /// </summary>
        /// <value>The variety groups.</value>
        public ObservableCollection<Varieties> VarietyGroups
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the variety.
        /// </summary>
        /// <value>The variety.</value>//
        public Varieties Variety
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the block.
        /// </summary>
        /// <value>The block.</value>
        public Block Block
        {
            get;
            set;
        }

        /// <summary>
        /// Navigates to varieties.
        /// </summary>
        public void NavigateToVarieties()
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;

            navigationPage.PushAsync(new VarietiesInformationContentPage(this));
        }

        /// <summary>
        /// Navigates to blocks.
        /// </summary>
        public void NavigateToBlocks()
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;

            navigationPage.PushAsync(new BlocksInformationContentPage(this));
        }

        /// <summary>
        /// Navigates to block.
        /// </summary>
        public void NavigateToBlock()
        {
            var navigationPage = (NavigationPage)Application.Current.MainPage;

            navigationPage.PushAsync(new BlockInformationContentPage(this));
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Varieties inherites ObservableCollection filled with Block types.
        /// </summary>
        public class Varieties : ObservableCollection<Block>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Varieties"/> class.
            /// </summary>
            /// <param name="variety">The name of this variety.</param>
            /// <param name="shortVariety">The identifier of this variety.</param>
            /// <param name="color">Color of this variety.</param>
            public Varieties(string variety, string shortVariety, Color color)
            {
                this.Variety = variety;
                this.ShortVariety = shortVariety;
                this.Color = color;
            }

            /// <summary>
            /// Gets the variety.
            /// </summary>
            /// <value>The variety.</value>
            public string Variety
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the short variety.
            /// </summary>
            /// <value>The short variety.</value>
            public string ShortVariety
            {
                get;
                private set;
            }

            /// <summary>
            /// Gets the color.
            /// </summary>
            /// <value>The color.</value>
            public Color Color
            {
                get;
                private set;
            }
        }
    }
}