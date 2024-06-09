using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasketMovement : MonoBehaviour
{


    public TMP_Text AnswersLabel;
    public TMP_Text EquationText;
    public TMP_Text ScoreValue;
    public AudioSource BirdBackGroundSound;
    public AudioSource GetCoinSound;
    public AudioSource MissCoinSound;
    public Button ModeLabel;
    private string ModeNameText = "Mode Name"; // Text to set on the button

    private Rigidbody2D rb;
    private float movementSpeed = 1000;
    private int numberOfLabels = 2;
    private float minLabelSpacing = 250f;   // Min space between answers
    private int correctAnswer;
    private int wrongAnswer;
    private bool canGenerate = true;
    private int score = 0;
    private float delay = 4f;               // Delay between each generation in seconds
    private string Difficulty = "";
    private string Operation = "";


    private void Start()
    {
        if (BirdBackGroundSound != null)
        {
            BirdBackGroundSound.Play();
        }
        else
        {
            Debug.LogWarning("BirdBackGroundSound is not set");
        }

        rb = GetComponent<Rigidbody2D>();
        Difficulty = PlayerPrefs.GetString("Difficulty", "Easy"); // Default to Easy if not found
        Operation = PlayerPrefs.GetString("Operation");
        ModeNameText = Operation + " Mode";
        SetModeName();
    }
    private void SetModeName()
    {
        // Check if the ModeLabel reference is assigned
        if (ModeLabel != null)
        {
            // Get the Text component attached to the button
            TMP_Text buttonTextComponent = ModeLabel.GetComponentInChildren<TMP_Text>();

            // Check if the Text component is found
            if (buttonTextComponent != null)
            {
                // Set the text on the button
                buttonTextComponent.text = ModeNameText;
            }
            else
            {
                Debug.LogWarning("Text component not found on the button.");
            }
        }
        else
        {
            Debug.LogWarning("Button reference not assigned.");
        }
    }

    void Update()
    {

        HandleMovement();
        HandleEquationGeneration();

        if (score < 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("GameOverScene");
        }


    }
    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = Vector2.right * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = Vector2.left * movementSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void HandleEquationGeneration()
    {
        if (canGenerate)
        {
            GenerateEquationAndNumbers();
            canGenerate = false; // Prevent new generation until delay has passed
            Invoke("ResetGenerationFlag", delay); // Invoke method to reset generation flag after delay
        }
    }

    void ResetGenerationFlag()
    {
        canGenerate = true; // Allow new equation generation
    }

    void GenerateEquationAndNumbers()
    {
        int num1, num2;

        int maxRange = GetMaxRangeByDifficulty(Difficulty);
        int wrongRange = GetWrongRangeByDifficulty(Difficulty);

        GenerateNumbers(Operation, maxRange, out num1, out num2);

        correctAnswer = CalculateCorrectAnswer(Operation, num1, num2);
        wrongAnswer = GenerateWrongAnswer(wrongRange, correctAnswer);

        DisplayEquation(num1, num2);

        PlaceLabels(correctAnswer, wrongAnswer);
    }

    int GetMaxRangeByDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                return 10;
            case "Normal":
                return 50;
            case "Hard":
                return 100;
            case "Very Hard":
                return 200;
            default:
                return 0;
        }
    }

    int GetWrongRangeByDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                return 20;
            case "Normal":
                return 100;
            case "Hard":
                return 200;
            case "Very Hard":
                return 400;
            default:
                return 0;
        }
    }

    void GenerateNumbers(string operation, int maxRange, out int num1, out int num2)
    {
        // Initialize num1 & num2 to avoid compiler error
        num1 = 0;
        num2 = 0;

        switch (operation)
        {
            case "Addition":
                GenerateAddition(maxRange, out num1, out num2);
                break;
            case "Subtraction":
                GenerateSubtraction(maxRange, out num1, out num2);
                break;
            case "Multiplication":
                GenerateMultiplication(maxRange, out num1, out num2);
                break;
            case "Division":
                GenerateDivision(maxRange, out num1, out num2);
                break;
            default:
                // Handle invalid operation
                break;
        }
    }

    void GenerateAddition(int maxRange, out int num1, out int num2)
    {
        num1 = UnityEngine.Random.Range(0, maxRange);
        num2 = UnityEngine.Random.Range(0, maxRange);
    }

    void GenerateSubtraction(int maxRange, out int num1, out int num2)
    {
        do
        {
            num1 = UnityEngine.Random.Range(0, maxRange);
            num2 = UnityEngine.Random.Range(0, maxRange);
        } while (num1 < num2);
    }

    void GenerateMultiplication(int maxRange, out int num1, out int num2)
    {
        num1 = UnityEngine.Random.Range(0, maxRange);
        num2 = UnityEngine.Random.Range(0, maxRange);
    }

    void GenerateDivision(int maxRange, out int num1, out int num2)
    {
        do
        {
            num1 = UnityEngine.Random.Range(1, maxRange);
            num2 = UnityEngine.Random.Range(1, maxRange);
        } while (num1 % num2 != 0);
    }

    int CalculateCorrectAnswer(string operation, int num1, int num2)
    {
        switch (operation)
        {
            case "Addition":
                return num1 + num2;
            case "Subtraction":
                return num1 - num2;
            case "Multiplication":
                return num1 * num2;
            case "Division":
                return num1 / num2;
            default:
                return 0;
        }
    }

    int GenerateWrongAnswer(int wrongRange, int correctAnswer)
    {
        int wrongAnswer;
        do
        {
            wrongAnswer = UnityEngine.Random.Range(0, wrongRange);
        } while (wrongAnswer == correctAnswer);
        return wrongAnswer;
    }

    void DisplayEquation(int num1, int num2)
    {
        EquationText.text = $"{num1} {OperationSymbol()} {num2}";
    }

    string OperationSymbol()
    {
        switch (Operation)
        {
            case "Addition":
                return "+";
            case "Subtraction":
                return "-";
            case "Multiplication":
                return "*";
            case "Division":
                return "/";
            default:
                return "";
        }
    }

    void PlaceLabels(int correct, int wrong)
    {
        Canvas canvas = FindObjectOfType<Canvas>(); // Find the Canvas object in the scene

        if (canvas == null)
        {
            Debug.LogError("No Canvas found in the scene.");
            return;
        }

        for (int i = 0; i < numberOfLabels; i++)
        {
            Vector3 randomPosition;
            do
            {
                randomPosition = new Vector3(UnityEngine.Random.Range(-520f, 550f), 300, 0f);
            } while (!IsSpaceAvailable(randomPosition));

            TMP_Text newLabel = Instantiate(AnswersLabel, canvas.transform);
            newLabel.tag = "NumberLabel"; // Set the tag
            if (i == 0)
            {
                newLabel.text = correct.ToString();
            }
            else
            {
                newLabel.text = wrong.ToString();
            }
            newLabel.rectTransform.anchoredPosition = randomPosition;
        }
    }

    bool IsSpaceAvailable(Vector3 position)
    {
        TMP_Text[] existingLabels = FindObjectsOfType<TMP_Text>();
        foreach (TMP_Text label in existingLabels)
        {
            if (Vector3.Distance(label.rectTransform.anchoredPosition, position) < minLabelSpacing)
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "NumberLabel")
        {
            CheckAnswer(other.gameObject);
        }
    }

    private void CheckAnswer(GameObject labelObject)
    {
        TMP_Text label = labelObject.GetComponent<TMP_Text>();
        if (label != null)
        {
            int number;
            if (int.TryParse(label.text, out number))
            {
                if (number == correctAnswer)
                {
                    GetCoinSound.Play();
                    score++;
                    ScoreValue.text = score.ToString(); // Update score display
                }
                else
                {
                    MissCoinSound.Play();
                    score--;
                    if (score < 0)
                    {
                        SceneManager.LoadScene("GameOverScene");
                    }
                    else
                    {
                        ScoreValue.text = score.ToString(); // Update score display
                    }
                }
                Destroy(labelObject);

            }
            else
            {
                Debug.LogWarning("Label text is not a valid number: " + label.text);
            }
        }
        else
        {
            Debug.LogWarning("No TMP_Text component found on collided GameObject");
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("ChooseLevelScene");
    }
}