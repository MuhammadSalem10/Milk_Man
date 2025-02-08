﻿using MilkMan.Shared.Enums;


namespace MilkMan.Application.Parameters;

public class OrderQueryParameters
{
    public OrderStatus? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}