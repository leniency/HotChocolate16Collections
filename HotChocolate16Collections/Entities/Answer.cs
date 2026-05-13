namespace HotChocolate16Collections.Entities;

public class Answer
{
    public int Id { get; set; }

    public int ResponseId { get; set; }

    public int QuestionId { get; set; }

    public Response Response { get; set; } = default!;

    public Question Question { get; set; } = default!;
}
