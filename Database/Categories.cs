using System;
using System.Collections.Generic;

namespace TRBlog.Database;

public partial class Category
{
    public string Name { get; set; }

    public static Category[] GetAllCategories()
    {
        return new Category[]{
            new Category(){
                Name= "bicycle"
            },
            new Category(){
                Name= "code"
            },
            new Category(){
                Name= "gaming"
            },
            new Category(){
                Name= "hardware"
            },
            new Category(){
                Name= "life"
            },
            new Category(){
                Name= "review"
            }
        };
    }
}
