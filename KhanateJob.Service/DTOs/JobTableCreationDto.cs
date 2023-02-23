using KhanateJob.Domain.Enums;

namespace KhanateJob.Service.DTOs;

public class JobTableCreationDto
{
    public JobCategories Categories { get; set; }
    public string JobName { get; set; }
    public string City { get; set; }
    public string WhatTheyDo { get; set; }
    public string HowToBecomeOne { get; set; }
    public string Salary_Info { get; set; }
    public decimal Salary { get; set; }

}
