using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ListTD_Alpha.Models
{
    /// <summary>
    /// Класс, описывающий задачу
    /// </summary>
    public class UserTask 
    {
        long id;                // Id задачи
        string title;           // Название задачи
        string? description;     // Описание задачи
        DateTime createdDate;   // Дата создания задачи
        bool isCompleted;       // Флаг завершения задачи
        /// <summary>
        /// Конструктор задачи
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <param name="title">Название задачи</param>
        /// <param name="description">Описание задачи</param>
        public UserTask(long id, string title, string description)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            createdDate = DateTime.Now;
            isCompleted = false;
        }
        /// <summary>
        /// Id задачи
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id обязательно")]
        public long Id { get { return id; } set { id = value; } }
        /// <summary>
        /// Название задачи
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Название обязательно")]
        public string Title { get { return title; } set { title = value; } }
        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get { return description; } set { description = value; } }
        /// <summary>
        /// Дата создания задачи
        /// </summary>
        public DateTime CreatedDate { get { return createdDate; } }
        /// <summary>
        /// Флаг завершения задачи
        /// </summary>
        [DefaultValue(false)]
        public bool IsCompleted { get { return isCompleted; } set { isCompleted = value; } }

    }
}
