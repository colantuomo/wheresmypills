using System.Collections;
using System.Collections.Generic;

public class GameInfo
{
    private float playerLife = 0f;

    public void SetPlayerLife(float life)
    {
        playerLife = life;
    }

    public float GetPlayerLife()
    {
        return playerLife;
    }
}
