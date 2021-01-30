
using UnityEngine.Events;

public static class EventManager
{
    public static Change2English EnglishEvent = new Change2English();

    public static Change2Chinese ChineseEvent = new Change2Chinese();
}

public class Change2English : UnityEvent { }

public class Change2Chinese : UnityEvent { }