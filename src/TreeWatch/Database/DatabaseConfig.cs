// <copyright file="DatabaseConfig.cs" company="TreeWatch">
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
    /// <summary>
    /// Database config keeps information about the database.
    /// </summary>
    public class DatabaseConfig : BaseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.DatabaseConfig"/> class.
        /// </summary>
        /// <param name="isset">If set to <c>true</c> database is already initialized.</param>
        public DatabaseConfig(bool isset)
            : this()
        {
            this.Init = isset;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeWatch.DatabaseConfig"/> class.
        /// </summary>
        public DatabaseConfig()
        {
            this.ID = 1;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreeWatch.DatabaseConfig"/> is initialized.
        /// </summary>
        /// <value><c>true</c> if init; otherwise, <c>false</c>.</value>
        public bool Init { get; set; }
    }
}