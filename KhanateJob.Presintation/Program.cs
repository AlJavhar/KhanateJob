using KhanateJob.DAL.IRepositories;
using KhanateJob.DAL.Repositories;
using KhanateJob.Domain.Entities;
using KhanateJob.Service.DTOs;
using KhanateJob.Domain.Enums;
using KhanateJob.Service.Services;

JobTableCreationDto job1 = new JobTableCreationDto()
{
    Categories = JobCategories.softwareDeveloper,
    City = "Tashkent",
    HowToBecomeOne = "A",
    JobName = "B",
    Salary_Info = "C",
    WhatTheyDo = "D",
    Salary = 100
};

ResumeCreationDto resume1 = new ResumeCreationDto()
{
    UserId = 10,
    Activities = "Q",
    Education = "W",
    Experince = "E",
    JobTableId = 1,
    Objective = "R",
    Salary = 210
};

ResumeCreationDto resume2 = new ResumeCreationDto()
{
    UserId = 20,
    Activities = "a",
    Education = "s",
    Experince = "d",
    JobTableId = 1,
    Objective = "f",
    Salary = 200
};

ResumeCreationDto resume3 = new ResumeCreationDto()
{
    UserId = 30,
    Activities = "z",
    Education = "x",
    Experince = "c",
    JobTableId = 1,
    Objective = "v",
    Salary = 100
};

IGenericRepository<Resumes> genericRepositoryResume = new GenericRepository<Resumes>();
IGenericRepository<JobTables> genericRepositoryJobTables = new GenericRepository<JobTables>();
IGenericRepository<CheckedJobs> genericRepositoryCheckedJobs = new GenericRepository<CheckedJobs>();

JobService jobService = new JobService(genericRepositoryJobTables);
ResumeService resumeService = new ResumeService(genericRepositoryResume, genericRepositoryCheckedJobs, genericRepositoryJobTables);

await jobService.CreateAsync(job1);
await resumeService.CreateAsync(resume1);
await resumeService.CreateAsync(resume2);
await resumeService.CreateAsync(resume3);

var result = await resumeService.GetSuitableResume(1);

Console.WriteLine(result.Value.UserId);