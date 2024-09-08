using System;
using System.Collections.Generic;

namespace E_CommerceProject7.Models;

public partial class Voucher
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public decimal DiscountAmount { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool? IsActive { get; set; }
}
