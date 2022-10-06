﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.DB.Models;

public class ChatMessage
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Room { get; set; } = null!;
    public string User { get; set; } = null!;

    public string Message { get; set; } = null!;

}