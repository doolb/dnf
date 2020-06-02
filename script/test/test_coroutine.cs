using Core;
using Godot;
using System.Collections;

public class test_coroutine : Node
{
    public override void _Ready() {
        this.StartCoroutine(testTime());
        time = this.StartCoroutine(testTime2());
    }

    IEnumerator time;
    IEnumerator testTime() {
        Debug.Log("yield start " + Time.time + " " + Time.frameCount);
        yield return new WaitForSeconds(3f);
        Debug.Log("yield finsh " + Time.time + " " + Time.frameCount);

        this.StopCoroutine(time);
        this.StartCoroutine(testTime2());
        this.StopAllCoroutine();
        Debug.Log("yield start " + Time.time + " " + Time.frameCount);
        yield return new WaitForFrame(30);
        Debug.Log("yield finsh " + Time.time + " " + Time.frameCount);

        yield return null;
    }
    IEnumerator testTime2() {
        Debug.Log("2 yield start " + Time.time + " " + Time.frameCount);
        yield return new WaitForSeconds(1f);
        Debug.Log("2 yield finsh " + Time.time + " " + Time.frameCount);

        Debug.Log("2 yield start " + Time.time + " " + Time.frameCount);
        yield return new WaitForFrame(300);
        Debug.Log("2 yield finsh " + Time.time + " " + Time.frameCount);

        yield return null;
    }
    
    IEnumerator testTime3() {
        Debug.Log("3 yield start " + Time.time + " " + Time.frameCount);
        yield return new WaitForSeconds(1.5f);
        Debug.Log("3 yield finsh " + Time.time + " " + Time.frameCount);

        Debug.Log("3 yield start " + Time.time + " " + Time.frameCount);
        yield return new WaitForFrame(300);
        Debug.Log("3 yield finsh " + Time.time + " " + Time.frameCount);

        yield return null;
    }
}