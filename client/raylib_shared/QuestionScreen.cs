using System.Numerics;
using Raylib_cs;
using Riskuj.Core;
using static Raylib_cs.Raylib;
namespace Riskuj.RaylibShared;

public class QuestionScreen(RiskujRaylib riskuj) : IScreen
{
    Font font = LoadFont(AppDomain.CurrentDomain.BaseDirectory + "font.ttf");
    Button back = null!;
    Button answerButton = null!;
    TextBox answer = null!;
    TextBox questionBox = null!;
    public Question question = null!;
    public bool answerVisible = false;
    public void Draw(float dt)
    {
        back.Draw();
        if (!answerVisible) answerButton.Draw();
        questionBox.Draw();
        if (answerVisible) answer.Draw();
    }

    public void Init()
    {
        answerVisible = false;
        Vector2 screenSize = new Vector2(GetScreenWidth(), GetScreenHeight());
        Vector2 size = new Vector2(120, 40);
        back = new()
        {
            rectangle = new Rectangle(screenSize.X - size.X, screenSize.Y - size.Y, size),
            color = Color.Yellow,
            text = "Back",
            onPressed = () =>
            {
                question.answered = true;
                riskuj.current_screen = riskuj.mainScreen;
                riskuj.current_screen.Init();
                riskuj.teamOverlay.Init();
            }
        };
        size = new Vector2(200, 40);
        answerButton = new()
        {
            rectangle = new Rectangle(0, screenSize.Y - size.Y, size),
            color = Color.Green,
            text = "Answer",
            onPressed = () => answerVisible = true,
        };
        size = new Vector2(screenSize.X / 1.3f, screenSize.Y / 5);
        questionBox = new()
        {
            rectangle = new Rectangle(screenSize.X / 2 - size.X / 2, screenSize.Y / 12, size),
            color = Color.White,
            text = question.question
        };
        answer = new()
        {
            text = question.answer,
            rectangle = new Rectangle(screenSize.X / 2 - size.X / 2, screenSize.Y / 2 - MeasureTextEx(font, question.answer, 40, 5).Y / 2, size),
            color = Color.Green,
        };
    }

    public void Update(float dt)
    {
        back.Update();
        answerButton.Update();
    }
}