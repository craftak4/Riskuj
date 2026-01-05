using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Riskuj.RaylibShared;
public class MainScreen(RiskujRaylib riskuj) : IScreen
{
    List<Button> buttons = [];
    List<TextBox> headers = [

    ];
    public void Update(float dt)
    {
        UpdateButtons();
    }
    public void UpdateButtons()
    {
        foreach (Button button in buttons)
            button.Update();
    }
    public void Draw(float dt)
    {
        DrawButtons();
        DrawHeaders();
    }
    public void DrawButtons()
    {
        foreach (Button button in buttons)
            button.Draw();
    }
    public void DrawHeaders()
    {
        foreach (TextBox box in headers)
            box.Draw();
    }

    public void Init()
    {
        buttons = [];
        headers = [];
        int columns = riskuj.questions.Fields.Count();
        int rows = riskuj.questions.Points.Count();
        Vector2 screenSize = new Vector2(GetScreenWidth(), GetScreenHeight());
        Vector2 size = screenSize / 5;
        Vector2 offset = screenSize / 2 - size / 2;
        Vector2 element_size = new Vector2(size.X / columns, size.Y / rows);
        offset.Y += element_size.Y;
        Vector2 index = Vector2.Zero;
        foreach (int points in riskuj.questions.Points.OrderDescending().Reverse())
        {
            foreach (string qfield in riskuj.questions.Fields)
            {
                Console.WriteLine($"{points} {qfield}");
                if (!riskuj.questions.GetQuestion(qfield,points)!.answered)
                buttons.Add(new Button()
                {
                    rectangle = new Rectangle(offset.X + element_size.X * index.X, offset.Y + element_size.Y * index.Y, element_size),
                    color = Color.White,
                    text = $"{points}",
                    onPressed = () => { riskuj.questionScreen.question = riskuj.questions.GetQuestion(qfield,points) ?? throw new Exception("No such question..."); riskuj.current_screen = riskuj.questionScreen; riskuj.current_screen.Init(); riskuj.questionScreen.answerVisible = false; },
                });
                index.X ++;
            }
            index.Y++;
            index.X = 0;
        }
        int headerIndex = 0;
        foreach (string header in riskuj.questions.Fields)
        {
            headers.Add(new TextBox()
            {
                color = Color.Red,
                text = header,
                rectangle = new Rectangle(offset.X + element_size.X * headerIndex,offset.Y-element_size.Y,element_size)
            });
            headerIndex++;
        }
    }
}