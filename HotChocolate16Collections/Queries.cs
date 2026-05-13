using HotChocolate16Collections.Entities;

namespace HotChocolate16Collections;

[QueryType]
public class Queries
{
    [UseOffsetPaging]
    public IQueryable<Response> GetResponses()
    {
        return new List<Response>().AsQueryable();
    }

    [UseOffsetPaging]
    public IQueryable<Question> GetQuestions()
    {
        return new List<Question>().AsQueryable();
    }
}
