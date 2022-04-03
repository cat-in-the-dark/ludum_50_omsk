using TMPro;
using UnityEngine;

public class Paper : MonoBehaviour
{
    private static readonly string textTemplate =
        "Aliquam erat volutpat. Vestibulum accumsan pellentesque massa vel commodo. Phasellus luctus porta lorem sed euismod. Morbi non nisl rhoncus, gravida neque non, consequat est. Maecenas aliquet tristique mauris, nec aliquam diam consectetur pulvinar. Ut feugiat massa eget risus malesuada, vel sagittis enim bibendum. Nullam dignissim nec elit vel faucibus. Duis tincidunt tincidunt libero congue suscipit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae. Vivamus leo nisl, pharetra vel dui at, consequat hendrerit eros. Nullam ante magna, eleifend vitae ultrices eu, interdum sit amet nibh. Sed a congue magna, in laoreet ex.  Sed rutrum, neque tempus euismod malesuada, eros mi viverra nisi, nec accumsan ex arcu ut ipsum. Aenean mattis et odio ac faucibus. Duis ac ex quis neque feugiat tincidunt. Nunc ex libero, commodo id cursus id, eleifend ut sapien. Cras nec urna eget justo consectetur iaculis. Morbi et arcu tellus. Donec iaculis, ligula id sagittis mattis, leo tortor scelerisque leo, consequat porttitor dolor ex vitae arcu. Vivamus cursus rutrum dolor sed congue. Mauris nec urna ac neque rutrum condimentum.  Suspendisse quis dignissim velit. Cras non enim ultrices, finibus nisl nec, maximus turpis. Donec sed arcu fringilla, iaculis sapien aliquam, elementum mi. Curabitur at orci mollis, lobortis ipsum et, finibus erat. Maecenas non urna eget dui pulvinar interdum facilisis at orci. Donec consequat risus nec pretium faucibus. Etiam varius purus tempor, scelerisque risus ac, scelerisque felis. Nam posuere, nulla in convallis rhoncus, dolor nisi venenatis nulla, quis molestie lectus tortor sed mi. Vivamus magna dolor, venenatis eget erat posuere, iaculis aliquet arcu. Nullam tincidunt eros ipsum, vitae placerat felis eleifend vel. Maecenas nibh mauris, sodales non sagittis quis, fringilla ut ipsum. Integer sed ante nulla.  Integer et tristique sem, efficitur venenatis tortor. Phasellus convallis diam orci, ac fermentum tellus consequat non. Ut rhoncus porta leo. Sed et commodo felis. Nam fermentum ex in felis elementum semper. Integer lorem tortor, pellentesque nec lacinia ac, posuere et ligula. Morbi feugiat dolor mi, id blandit nulla accumsan sit amet. Sed vehicula placerat arcu aliquet egestas. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Integer tortor leo, vehicula et dui et, ullamcorper aliquet neque. Phasellus congue nulla ex, vulputate molestie dolor imperdiet non. Maecenas ut erat nec ligula semper pharetra. Integer sollicitudin enim vitae leo ullamcorper molestie. Cras sed sagittis mauris, id lobortis lorem. Pellentesque porttitor purus in mauris tempus fermentum. Praesent porta sapien eu nulla tincidunt, in venenatis elit molestie.  Vivamus sed massa nec arcu sollicitudin elementum ut id lorem. Nunc non nunc dignissim sapien efficitur vulputate non eu risus. Duis tincidunt odio id aliquet vulputate. Fusce vestibulum egestas massa. Mauris sit amet erat volutpat, consequat lectus quis, posuere neque. Nam quis tortor dictum, porttitor lectus id, rhoncus metus. Nunc tincidunt posuere pulvinar. Cras rhoncus malesuada orci, tincidunt tincidunt augue tincidunt ac. Morbi et dolor pulvinar, aliquam lectus quis, porttitor risus. Proin non condimentum metus. Vestibulum vestibulum magna nec turpis porttitor, vitae porttitor velit porttitor. Suspendisse sed nunc bibendum, sodales ante ut, blandit diam. Etiam et ligula diam. Curabitur sit amet magna convallis augue pellentesque tristique eu vitae neque. Pellentesque tempor ex faucibus tempor eleifend.  Morbi et libero eget leo malesuada ornare et nec enim. Morbi iaculis dolor ut arcu sagittis gravida. Nam mollis ex non metus semper, non sollicitudin libero imperdiet. Proin vel mauris mauris. Nullam mauris ex, venenatis ut hendrerit suscipit, venenatis ac ipsum. Vivamus sem elit, eleifend eget dui in, consectetur semper felis. Proin vulputate eu nibh non feugiat. Nullam facilisis lobortis lorem, eu varius lacus vulputate aliquam. Praesent volutpat pulvinar tempor. Etiam ut leo lobortis, tristique turpis ut, tristique purus. Donec at dictum magna, vitae pulvinar quam.  Nam erat arcu, sagittis in elementum quis, interdum vel nibh. Aenean eu metus urna. In odio purus, ornare congue enim consectetur, varius efficitur tortor. Donec porttitor nisl ut dolor accumsan, at congue risus finibus. Sed at pharetra ante. Duis sit amet elit nisl. Nam vestibulum, orci sed pharetra molestie, ipsum felis molestie nisl, vel varius mi justo id ligula. Etiam ante justo, consectetur a tempus sagittis, vehicula et massa. Sed efficitur molestie arcu vel faucibus. Aenean tristique vitae sem ut consectetur. Quisque a sem ornare, tempus mi eu, rhoncus dui. Integer nunc tellus, cursus ac tristique vitae, euismod volutpat ipsum. Fusce nec viverra nunc. Nullam sed placerat dui. Proin vitae sapien sollicitudin, suscipit mauris non, commodo quam. Sed volutpat lectus eu ligula commodo laoreet. Praesent et aliquam leo, a nulla.";

    private string Text => textTemplate.Substring(0, currentPos);
    
    [SerializeField] private TMP_Text uiText;
    [SerializeField] private int charsPerUpdate;
    private readonly int maxChars = 3500;
    public float CurrentProgress => Mathf.Clamp01((float) currentPos / maxChars);

    private int pos;
    private int currentPos;

    private bool isTyping;
    
    private State state;

    private void Start()
    {
        state = State.Find();
        pos = 0;
        currentPos = 0;
        isTyping = false;
    }

    private void Update()
    {
        if (currentPos < pos)
        {
            uiText.text = Text;
            currentPos += charsPerUpdate;
        }
        else
        {
            isTyping = false;
        }
    }


    public void AppendText()
    {
        if (state.energyLevel <= state.minEnergyLevel) return;
        if (!state.isLampEnabled) return;
        if (isTyping) return;
        if (state.inHand != State.HandObjects.PENCIL) return;

        isTyping = true;
        var step = Mathf.CeilToInt(state.typingSpeed * maxChars);
        pos += step;
        state.energyLevel -= state.workCost;
    }
}