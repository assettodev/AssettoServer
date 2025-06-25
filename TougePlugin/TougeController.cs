using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace TougePlugin;



[ApiController]
[Route("touge")]
public class TougeController : ControllerBase
{
    private static readonly string AssetsBasePath = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets");

    [HttpGet("Elo.png")]
    public IActionResult GetEloImage()
    {
        return new PhysicalFileResult(Path.Join(AssetsBasePath, "Elo.png"), "image/png");
    }

    [HttpGet("InviteMenu.png")]
    public IActionResult GetInviteMenuImage()
    {
        return new PhysicalFileResult(Path.Join(AssetsBasePath, "InviteMenu.png"), "image/png");
    }

    [HttpGet("Key.png")]
    public IActionResult GetKeyImage()
    {
        return new PhysicalFileResult(Path.Join(AssetsBasePath, "Key.png"), "image/png");
    }

    [HttpGet("MKey.png")]
    public IActionResult GetMKeyImage()
    {
        return new PhysicalFileResult(Path.Join(AssetsBasePath, "MKey.png"), "image/png");
    }

    [HttpGet("PlayerCard.png")]
    public IActionResult GetPlayerCardImage()
    {
        return new PhysicalFileResult(Path.Join(AssetsBasePath, "PlayerCard.png"), "image/png");
    }

    [HttpGet("Standings.png")]
    public IActionResult GetStandingsImage()
    {
        return new PhysicalFileResult(Path.Join(AssetsBasePath, "Standings.png"), "image/png");
    }

    [HttpGet("Tutorial.png")]
    public IActionResult GetTutorialImage()
    {
        return new PhysicalFileResult(Path.Join(AssetsBasePath, "Tutorial.png"), "image/png");
    }

    [HttpGet("fonts.zip")]
    public IActionResult GetFontsZip()
    {
        return new PhysicalFileResult(Path.Join(AssetsBasePath, "fonts.zip"), "application/zip");
    }



}
