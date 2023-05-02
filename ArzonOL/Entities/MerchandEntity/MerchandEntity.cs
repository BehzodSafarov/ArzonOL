using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ArzonOL.Entities;

public class MerchandEntity : IdentityUser
{
    public List<BaseProductEntity>? Products { get; set; }
}