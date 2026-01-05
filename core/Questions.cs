using Newtonsoft.Json;

namespace Riskuj.Core;

public class QuestionSystem(params IEnumerable<QuestionField> questions)
{
	readonly IEnumerable<QuestionField> questions = questions;
	public IEnumerable<string> Fields => questions.Select(x => x.field);
	public IEnumerable<int> Points
	{
		get
		{
			List<int> points = new();
			foreach (QuestionField questionField in questions)
				foreach (Question question in questionField.questions)
					points.Add(question.points);
			return points.Distinct();
		}
	}
	public static QuestionSystem FromJson(string path) => new(questions: JsonConvert.DeserializeObject<QuestionField[]>(new StreamReader(path).ReadToEnd()) ?? []);
	public Question? GetQuestion(string field, int points)
	{
		foreach (QuestionField fieldQuestions in questions)
			if (fieldQuestions.field == field)
				foreach (Question question in fieldQuestions.questions)
					if (question.points == points)
						return question;
		return null;
	}
}

public record struct QuestionField
{
	[JsonRequired] public required string field;
	[JsonRequired] public required Question[] questions;
}

public class Question()
{
	[JsonRequired] public required string question;
	public string answer = "";
	[JsonRequired] public required int points;
	[JsonIgnore] public bool answered = false;
}


