using HotChocolate16Collections.Entities;

namespace HotChocolate16Collections;

/*
 *  Nested types.
 *   - Response -> Answer
 *   - Question -> Answer
 *  
 *  Add a paged field to the parent type. It should generate a single
 *  AnswerCollectionSegment type, but instead if duplicates and throws
 *  an exception.
 *  
 *  The same issue happens if we [UsePaging]
 */



[ObjectType<Response>]
public static partial class ResponseType
{
    [UseOffsetPaging]
    public static IQueryable<Answer> Answers([Parent(nameof(Response.Id))] Response response)
        => new List<Answer>().AsQueryable();
}


[ObjectType<Question>]
public static partial class QuestionType
{
    [UseOffsetPaging]
    public static IQueryable<Answer> Answers([Parent(nameof(Question.Id))] Question question)
        => new List<Answer>().AsQueryable();
}