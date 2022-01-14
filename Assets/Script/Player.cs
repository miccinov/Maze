using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public Sounds[] sounds;
    private bool flagVictory;
    private Coroutine win;

    private void Awake()
    {
        foreach (Sounds s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
         }
        flagVictory = false;

    }
    void Start()
    {
        win=StartCoroutine(checkWin());
        sounds[2].source.loop = true;
        sounds[2].source.PlayDelayed(1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void moveUP() {
        transform.Translate(0,0,1);
       sounds[0].source.Play();
        sounds[0].source.PlayDelayed(0.5f);
    }

    public void moveDown()
    {
        transform.Translate(0,0,-1);
        sounds[0].source.Play();
        sounds[0].source.PlayDelayed(0.5f);
    }

    public void moveLeft() {
        transform.Translate(-1,0,0);
        sounds[0].source.Play();
        sounds[0].source.PlayDelayed(0.5f);
    }

    public void moveRight() {
        transform.Translate(1,0,0);
        sounds[0].source.Play();
        sounds[0].source.PlayDelayed(0.5f);
    }

    public void startSoundVictory() {
        sounds[1].source.Play();
            }
    private bool checkEndPosition() { return (transform.position.x == GameManager.current.endPos.x) && (transform.position.z == GameManager.current.endPos.y);  }

    public IEnumerator checkWin() {

        while (!flagVictory) {

            if (checkEndPosition())
            {
                startSoundVictory();
                flagVictory = true;
            }
            yield return new WaitForSeconds(0.5f);
        }
        
    
    }
}
