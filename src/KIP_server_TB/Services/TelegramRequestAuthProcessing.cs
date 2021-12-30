using System.Linq;
using System.Text;
using KIP_Backend.Constants;
using KIP_Backend.Models.Auth;

namespace KIP_server_TB.Services
{
    /// <summary>
    /// Telegram request auth processing.
    /// </summary>
    public static class TelegramRequestAuthProcessing
    {
        /// <summary>
        /// Get student personal information.
        /// </summary>
        /// <param name="studentInfo">The studentInfo.</param>
        /// <returns>Output string representation.</returns>
        public static string GetStudentPersonalInformation(PersonalInformation studentInfo)
        {
            var facultyShortName = FacultiesShortNames.ShortNames.FirstOrDefault(f => studentInfo.Faculty.Contains(f.Key)).Value;
            var outputSb = new StringBuilder();

            outputSb.AppendLine("Особистий кабінет студента\n");

            outputSb.AppendLine($"Прізвище: {studentInfo.LastName}");
            outputSb.AppendLine($"Ім'я: {studentInfo.FirstName}");
            outputSb.AppendLine($"По-батькові: {studentInfo.Patronymic}\n");

            outputSb.AppendLine($"Курс: {studentInfo.Course}");
            outputSb.AppendLine($"Група: {studentInfo.Group}");
            outputSb.AppendLine($"Факультет/інститут: {studentInfo.Faculty} ({facultyShortName ?? string.Empty})");
            outputSb.AppendLine($"Кафедра: {studentInfo.Cathedra}\n");

            outputSb.AppendLine($"Спеціалізація/пропозиція/блок: {studentInfo.Specialization}");
            outputSb.AppendLine($"Спеціальність: {studentInfo.Specialty}");
            outputSb.AppendLine($"Освітня програма: {studentInfo.StudyingProgram}");
            outputSb.AppendLine($"Рівень навчання: {studentInfo.StudyingLevel}");
            outputSb.AppendLine($"Форма навчання: {studentInfo.StudyingForm}");
            outputSb.AppendLine($"Оплата: {studentInfo.BudgetForm}");

            return outputSb.ToString();
        }
    }
}
