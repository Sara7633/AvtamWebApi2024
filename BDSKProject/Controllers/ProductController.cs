using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Tamar_Sheva_Project.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class ProductController : ControllerBase
    { 


            private IProductService _productService;
            private IMapper _mapper;
            public ProductController(IProductService productService, IMapper mapper)
            {
            _productService = productService;
            _mapper = mapper;
            }
            [HttpGet]
            public async Task<ActionResult<List<ProductDTO>>> Get(string? descreption,int? min,int? max,string? name,[FromQuery] int?[] categoryIds, int position = 20, int skip = 1)
            {
           
                List<Product> Result = await _productService.Get(descreption,min, max, name, categoryIds,position = 35, skip = 1);
                List<ProductDTO> productDto = _mapper.Map<List<Product>, List<ProductDTO>>(Result);
                if (Result != null)
                {
                    return Ok(productDto);
                }

                return BadRequest(); 
            }

    }

}






