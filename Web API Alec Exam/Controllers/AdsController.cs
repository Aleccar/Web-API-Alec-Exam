using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_Alec_Exam.Data;
using Web_API_Alec_Exam.Models;

namespace Web_API_Alec_Exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AdsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // List all Ads
        [HttpGet]
        public async Task<ActionResult<List<Ad>>> GetAll()
        {
            return Ok(await _dbContext.Ads.ToListAsync());
        }

        // Get one ad by Id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ad>> GetOne(int id)
        {
            var ad = await _dbContext.Ads.FirstOrDefaultAsync(a => a.Id == id);
            if (ad == null)
            {
                return NotFound();
            }
            return Ok(ad);
        }

        // Create new ad
        [HttpPost]
        public async Task<ActionResult<Ad>> PostAd(NewAd newAd)
        {
            Ad ad = new Ad
            {
                Title = newAd.Title,
                Description = newAd.Description,
                CreatedAt = DateTime.Now,
                Price = newAd.Price,
            };
            _dbContext.Add(ad);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOne), new { id = ad.Id }, ad);
        }

        // Update Ad
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Ad>> UpdateAd(int id, UpdateAd updatedAd)
        {
            var adToUpdate = await _dbContext.Ads.FirstOrDefaultAsync(a => a.Id == id);
            if (adToUpdate == null)
            {
                return NotFound();
            }

            adToUpdate.Title = updatedAd.Title;
            adToUpdate.Description = updatedAd.Description;
            adToUpdate.Price = updatedAd.Price;

            await _dbContext.SaveChangesAsync();
            return Ok(adToUpdate);
        }

        // Delete an Ad
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAd(int id)
        {
            var adToDelete = await _dbContext.Ads.FirstOrDefaultAsync(a => a.Id == id);
            if (adToDelete == null)
            {
                return NotFound();
            }

            _dbContext.Ads.Remove(adToDelete);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        // Update PART of an Ad
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Ad>> PatchAd(JsonPatchDocument<Ad> ad, int id)
        {
            var adToPatch = await _dbContext.Ads.FindAsync(id);

            if (adToPatch == null)
            {
                return NotFound();
            }

            if (ad.Operations.Any(o => 
            (o.path?.StartsWith("/Id", StringComparison.OrdinalIgnoreCase) ?? false) ||
            (o.path?.StartsWith("/CreatedAt", StringComparison.OrdinalIgnoreCase) ?? false) ||
            (o.from?.StartsWith("/Id", StringComparison.OrdinalIgnoreCase) ?? false) ||
            (o.from?.StartsWith("/CreatedAt", StringComparison.OrdinalIgnoreCase) ?? false)))
            {
                return BadRequest("Id and CreatedAt cannot be modified.");
            }


            ad.ApplyTo(adToPatch, error =>
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            });

            if (!ModelState.IsValid)
            {
                return BadRequest("Improper input");
            }

            if (!TryValidateModel(adToPatch))
            {
                return BadRequest();
            }

            await _dbContext.SaveChangesAsync();
            return Ok(adToPatch);
        }
    }
}
