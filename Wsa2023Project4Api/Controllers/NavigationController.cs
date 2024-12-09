using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Buffers.Text;
using System.Text;
using Wsa2023Project4Api.Models;

namespace Wsa2023Project4Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NavigationController : Controller
    {
        


        AdditionalPhContext context = new AdditionalPhContext();

        public class LoginModel
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        [HttpPost("login")]
        public IActionResult Login(LoginModel login)
        {
            try
            {
                var isvalidUser = context.Tblusers
               .Where(x => x.Username == login.username && x.Password == login.password)
               .FirstOrDefault();

                if (isvalidUser != null)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {

                return NotFound();
            }




        }


        public class TempTouristSpot
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Description { get; set; }
            public string? Picture { get; set; }
            public string Rating { get; set; }
            public string EntranceFee { get; set; }
        }

        [HttpGet("gettouristspot/{name}")]
        public IActionResult getTouristSpot(string name)
        {
            var id = context.TblMunicipalities
                .Where(x => x.MunName == name)
                .Select(x => x.Id)
                .FirstOrDefault();


            var touristSpots = context.TblTouristSpots
                .Where(x => x.MunId == id)
                .Select(x => new
                {
                    id = x.Id,
                    name = x.Tname,
                    address = x.Address,
                    description = x.SpotDescription,
                    picture = x.Picture,
                    rating = x.Rating,
                    entranceFee = x.Entrancefee,
                  
                })
                .ToList();

            var tempTouristSpots = new List<TempTouristSpot>();

            foreach (var touristSpot in touristSpots)
            {
                var tempTouristSpot = new TempTouristSpot
                {
                    Id = touristSpot.id,
                    Name = touristSpot.name,
                    Address = touristSpot.address,
                    Description = touristSpot.description,
                    Rating = touristSpot.rating,
                    EntranceFee = touristSpot.entranceFee
                };


                if (!string.IsNullOrEmpty(touristSpot.picture))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", touristSpot.picture);
                    if (!touristSpot.picture.EndsWith(".png"))
                    {
                        filePath += ".png";
                    }

                    if (System.IO.File.Exists(filePath))
                    {
                        byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                        tempTouristSpot.Picture = base64ImageRepresentation;
                        //using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        //using (var ms = new MemoryStream())
                        //{
                        //    fileStream.CopyTo(ms);
                        //    tempTouristSpot.Picture = Convert.ToBase64String(ms.ToArray());
                        //}
                    }
                    else
                    {

                    }
                }

                tempTouristSpots.Add(tempTouristSpot);



            }







            return Ok(tempTouristSpots);
        }


        [HttpGet]
        public IActionResult getMunicipalities()
        {
            var municipalities = context.TblMunicipalities
                .Select(x => new
                {
                    id = x.Id,
                    name = x.MunName,
                    map = Encoding.UTF8.GetString(x.MunMap),
                    logo = "",
                    description = x.MunDescription,
                    touristSpot = x.TblTouristSpots.Count().ToString()
                })
                .ToList();

         


            return Ok(municipalities);


        }

        [HttpGet("getmunicipality/{name}")]
        public IActionResult getMunicipality(string name)
        {
            var id = context.TblMunicipalities
                .Where(x => x.MunName == name)
                .Select(x => x.Id)
                .FirstOrDefault();
            var municipalities = context.TblMunicipalities
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    id = x.Id,
                    name = x.MunName,
                    map = Encoding.UTF8.GetString(x.MunMap),
                    logo = Encoding.UTF8.GetString(x.Logo),
                    description = x.MunDescription,
                    touristSpot = x.TblTouristSpots.Count().ToString()
                })
                .FirstOrDefault();




            return Ok(municipalities);


        }

        [HttpGet("getmunicipalitymap/{name}")]
        public IActionResult getMunicipalityMap(string name)
        {
            var municipalities = context.TblMunicipalities
                .Where(x => x.MunName == name)
                .Select(x => new
                {
                    id = x.Id,
                    name = x.MunName,
                    map = Encoding.UTF8.GetString(x.MunMap),
                    logo = "",
                    description = x.MunDescription,
                    touristSpot = x.TblTouristSpots.Count().ToString()
                })
                .FirstOrDefault();




            return Ok(municipalities);


        }


    }


    public class Converter
    {
        public static string HexToBase64(string hexString)
        {
            // Remove the '0x' prefix if it exists
            if (hexString.StartsWith("0x"))
            {
                hexString = hexString.Substring(2);
            }

            // Convert Hex string to byte array
            byte[] bytes = Enumerable.Range(0, hexString.Length / 2)
                                      .Select(x => Convert.ToByte(hexString.Substring(x * 2, 2), 16))
                                      .ToArray();

            // Convert byte array to Base64 string
            return Convert.ToBase64String(bytes);
        }
    }

}
