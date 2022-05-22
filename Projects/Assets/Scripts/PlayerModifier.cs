using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModifier : MonoBehaviour
{
    [SerializeField]
    MeshRenderer Player;
    [SerializeField]
    Slider[] ColorSlider,ShapeSlider;
    [SerializeField]
    Transform PlayerParent;

   
    private void Start()
    {
        Constants.PlayerScale = PlayerParent.localScale;
        Constants.ColorProp = Player.material.color;
    }


    public void OnEditColor()
    {
        
        Constants.ColorProp.r = ColorSlider[0].value;
        Constants.ColorProp.g = ColorSlider[1].value;
        Constants.ColorProp.b = ColorSlider[2].value;
        Player.material.color = Constants.ColorProp;
        Player.material.SetColor("_EmissionColor", Constants.ColorProp);
    }
    public void OnEditShape()
    {

        Constants.PlayerScale.x = ShapeSlider[0].value;
        Constants.PlayerScale.y = ShapeSlider[1].value;
        PlayerParent.localScale = Constants.PlayerScale;

    }

   public void OnClickStart()
    {
        Application.LoadLevel("Game");
    }
    public void OnClickBack()
    {
        LoadAsset.assetBundle.Unload(true);
        Application.LoadLevel("Menu");
    }
}
