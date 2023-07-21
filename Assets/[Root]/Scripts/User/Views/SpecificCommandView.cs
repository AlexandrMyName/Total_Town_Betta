using UnityEngine;
using UnityEngine.UI;

public class SpecificCommandView : MonoBehaviour
{
     [SerializeField] private CmdExe<IBuildProccess> cmdExe;
     public Button SpecificButton;
     public IExecutor Executor => cmdExe;


    public float TimeValueInSeconds;

    public void CompleteStart()
    {
        SpecificButton.onClick.RemoveAllListeners();
        SpecificButton.interactable = false;
    }
}
