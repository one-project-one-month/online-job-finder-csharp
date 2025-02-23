﻿namespace online_job_finder.Domain.ViewModels;

public class UsersViewModels
{
    public string Username { get; set; } = null!;

    public string ProfilePhoto { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid RoleId { get; set; }

    public bool IsInformationCompleted { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
