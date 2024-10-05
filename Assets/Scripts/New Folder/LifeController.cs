using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public LifeModel lifeModel;
    public LifeView lifeView;


    // Start is called before the first frame update
    void Start()
    {
        
        
        lifeModel.LifeModelStart();


        lifeView?.UpdateLivesText(lifeModel.Lives);
        lifeView?.UpdateTimer(lifeModel);  // Start updating the timer
    }

    // Update is called once per frame
    void Update()
    {
        lifeModel.UpdateLifeRegeneration();
        lifeView?.UpdateTimer(lifeModel);
        
        if (lifeModel.Lives <= LifeModel.MaxLives)
        {
            lifeView?.UpdateLivesText(lifeModel.Lives);
        }

    }


    public void OnPlayLevel()
    {
        if (lifeModel.Lives > 0)
        {
            lifeModel.UseLife();
            lifeView?.UpdateLivesText(lifeModel.Lives);
        }
        else
        {
            //Not enough lives to play!
            MainMenuManager.Instance?.ShowWaitForLives();
        }
    }


}
