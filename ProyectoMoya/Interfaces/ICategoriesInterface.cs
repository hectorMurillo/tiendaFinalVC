using ProyectoMoya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoMoya.Interfaces
{
    public interface ICategoriesInterface
    {
        void Create(Category newCategory);
        List<Category> getCategories();
        Category getCategoryId(string ID);
        List<Category> getSubCategories();
        void deleteCategory(Category category);
        void updateCategory(Category editCategory);
    }
}
