using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblerStats : MonoBehaviour
{
    // This class will track important stats for mini games

    private int guess;
    private int bet;

    public int Guess
    {
        get { return guess; }
        set { guess = value; }
    }

    public int Bet
    {
        get { return bet; }
        set { bet = value; }
    }

    public int RollDie()
    {
        return Random.Range(1, 6);
    }

    public string PlaySixShooter()
    {
        // get a string to fill into the text box somehow...
        string res = "";
        int roll = RollDie();

        res += $"A {roll} is rolled...";

        if (guess == roll)
        {
            GameManager.Instance.PlayerInfo.Money += bet * 4;
            res += $" You won {(bet * 5) / 100}.{((bet * 5) % 100).ToString("00")} dollars";
        }
        else
        {
            GameManager.Instance.PlayerInfo.Money -= bet;
            res += $" You lost {bet / 100}.{(bet % 100).ToString("00")} dollars";
        }
        return res;
    }
}
