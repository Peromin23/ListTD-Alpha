using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ListTD_Alpha.Models;

namespace ListTD_Alpha.Controllers 
{
    /// <summary>
    /// Класс Controller для ToDo List API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserTasksController : ControllerBase
    {
        private readonly ApplicationContext _context;
        /// <summary>
        /// Контекст для класса Controller
        /// </summary>
        /// <param name="context">Контекст</param>
        public UserTasksController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/UserTasks
        /// <summary>
        /// Отображение списка всех задач
        /// </summary>
        /// <returns>Список всех задач</returns>
        /// <response code="200">Возвращает список задач</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserTask>>> GetUserTasks()
        {
          if (_context.UserTasks == null)
          {
              return NotFound();
          }
            return await _context.UserTasks.ToListAsync();
        }

        // GET: api/UserTasks/5
        /// <summary>
        /// Отображение задачи с требуемым id
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <returns>Задача с требуемым id</returns>
        /// <response code="200">Возвращает задачу с требуемым id</response>
        /// <response code="404">Ошибка. Задача с требуемым id не найдена</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserTask>> GetUserTask(long id)
        {
          if (_context.UserTasks == null)
          {
              return NotFound();
          }
            var userTask = await _context.UserTasks.FindAsync(id);

            if (userTask == null)
            {
                return NotFound();
            }

            return userTask;
        }

        // PUT: api/UserTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Изменение задачи с требуемым id
        /// </summary>
        /// <param name="id">Id изменяемой задачи</param>
        /// <param name="userTask">Задача</param>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///         "id": 1,
        ///         "title": "Другое название",
        ///         "description": "Другое описание"
        ///         
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="204">Изменение выполненно успешно</response>
        /// <response code="400">Ошибка. Неверный запрос</response>
        /// <response code="404">Ошибка. Задача с требуемым id не найдена</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutUserTask(long id, UserTask userTask)
        {
            if (id != userTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(userTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Создание новой задачи
        /// </summary>
        /// <param name="userTask">Новая задача</param>
        /// <returns></returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///         "id": 1,
        ///         "title": "Название",
        ///         "description": "Описание"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Возвращает только созданный элемент</response>
        /// <response code="400">Ошибка. Запрос пустой</response>
        /// <response code="500">Ошибка. Ошибка сервера</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserTask>> PostUserTask(UserTask userTask)
        {
            _context.UserTasks.Add(userTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserTask), new { id = userTask.Id }, userTask);
        }

        // DELETE: api/UserTasks/5
        /// <summary>
        /// Удаляет задачу с требуемым id
        /// </summary>
        /// <param name="id">Id удаляемой задачи</param>
        /// <returns></returns>
        /// <response code="204">Удаление выполненно успешно</response>
        /// <response code="404">Ошибка. Задача с требуемым id не найдена</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserTask(long id)
        {
            if (_context.UserTasks == null)
            {
                return NotFound();
            }
            var userTask = await _context.UserTasks.FindAsync(id);
            if (userTask == null)
            {
                return NotFound();
            }

            _context.UserTasks.Remove(userTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Tasks/5
        /// <summary>
        /// Измениение статуса задачи с требуемым id на противоположное
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <returns></returns>
        /// <response code="204">Изменение выполненно успешно</response>
        /// <response code="404">Ошибка. Задача с требуемым id не найдена</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ToggleTaskStatus(int id)
        {
            UserTask userTask = _context.UserTasks.FirstOrDefault(t => t.Id == id);
            if (userTask == null)
            {
                return NotFound();
            }
            userTask.IsCompleted = !userTask.IsCompleted;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserTaskExists(long id)
        {
            return (_context.UserTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
