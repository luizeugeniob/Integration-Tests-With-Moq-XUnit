namespace ToDo.Core.Models
{
    /// <summary>
    /// Representa uma classificação dada a <see cref="ToDoTask"/>.
    /// </summary>
    public class Category
    {
        public Category(string name)
        {
            Name = name;
        }

        public Category(int id, string name) : this(name)
        {
            Id = id;
        }

        /// <summary>
        /// Identificador da categoria.
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// Nome da categoria.
        /// </summary>
        public string Name { get; private set; }
    }
}
