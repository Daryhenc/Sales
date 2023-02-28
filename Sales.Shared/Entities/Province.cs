using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class Province
    {
        public int ProvinceId { get; set; }
        [Display(Name = "provincia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Name { get; set; } = null!;
        public int CountryId { get; set; }
        public Country? Country { get; set; }

        public ICollection<City>? Cities { get; set; }
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;

    }
}
