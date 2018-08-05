using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Web.Models
{
    public class PatientGroupsForCalculateDto : IValidatableObject
    {
        [Required]
        public List<List<int>> Matrix { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Matrix != null && Matrix.Any() && Matrix[0].Any()) {
                var rows = Matrix.Count;
                var cols = Matrix[0].Count;

                if (!Matrix.TrueForAll(x => x.Count == cols)) {
                    yield return new ValidationResult(
                        $"Jagged matrix is not supported",
                        new[] { nameof(Matrix) });
                }
            }
        }

        public int[,] MapToMatrix() {
            var rows = Matrix.Count;
            var cols = Matrix[0].Count;
            var matrix = new int[rows,cols];

            for (var row = 0; row < rows; row++) {
                for (var col = 0; col < cols; col++) {
                    matrix[row, col] = Matrix[row][col];
                }
            }

            return matrix;
        }
    }
}
