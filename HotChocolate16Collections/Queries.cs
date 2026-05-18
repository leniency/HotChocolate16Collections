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


    // Works with static!
    [UseOffsetPaging]
    public static IQueryable<Answer> AllAnswers()
        => new List<Answer>().AsQueryable();


    // Error with non-static.
    // Uncomment for error.
    //[UseOffsetPaging]
    //public IQueryable<Answer> SomeAnswers()
    //    => new List<Answer>().AsQueryable();
}
