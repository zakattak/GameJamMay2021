using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using System.Linq;

public class Sound : MonoBehaviour
{
    public List<AudioClip> ambienceClips;
    public AudioSource ambienceSource;

    public List<AudioClip> pianoClips = new List<AudioClip>();
    public AudioSource pianoSource;

    public List<AudioClip> robinClips = new List<AudioClip>();
    public AudioSource robinSource;
    public int secondsTilNextRobin = 0;
    public int robinMinInterval = 15;
    public int robinMaxInterval = 225;

    public List<AudioClip> finchClips = new List<AudioClip>();
    public AudioSource finchSource;
    public int secondsTilNextFinch = 0;
    public int finchMinInterval = 7;
    public int finchMaxInterval = 230;

    public List<AudioClip> sparrowClips = new List<AudioClip>();
    public AudioSource sparrowSource;
    public int secondsTilNextSparrow = 0;
    public int sparrowMinInterval = 32;
    public int sparrowMaxInterval = 424;

    public List<AudioClip> gooseClips = new List<AudioClip>();
    public AudioSource gooseSource;
    public int secondsTilNextGoose = 0;
    public int gooseMinInterval = 240;
    public int gooseMaxInterval = 667;

    public List<AudioClip> whipClips = new List<AudioClip>();
    public AudioSource whipSource;
    public int secondsTilNextWhip = 0;
    public int whipMinInterval = 180;
    public int whipMaxInterval = 1000;

    System.Random random = new System.Random();
    UniRx.IObservable<long> tick = Observable.Interval(TimeSpan.FromSeconds(1)).AsObservable();

    public Datastore datastore;

    void Awake() {
        datastore = GameObject.Find("Datastore").GetComponent<Datastore>();

        (pianoSource, pianoClips) = loadClips(20, "SFX/piano/piano_", "D3");
        pianoSource.volume = 0.7f;

        (robinSource, robinClips) = loadClips(4, "SFX/birds/robin_");
        robinSource.volume = 0.28f;
        secondsTilNextRobin = random.Next(robinMinInterval, robinMaxInterval);

        (finchSource, finchClips) = loadClips(5, "SFX/birds/finch_");
        finchSource.volume = 0.24f;
        secondsTilNextFinch = random.Next(finchMinInterval, finchMaxInterval);

        (gooseSource, gooseClips) = loadClips(1, "SFX/birds/goose_");
        gooseSource.volume = 0.3f;
        secondsTilNextGoose = random.Next(gooseMinInterval, gooseMaxInterval);

        (sparrowSource, sparrowClips) = loadClips(3, "SFX/birds/sparrow_");
        sparrowSource.volume = 0.3f;
        secondsTilNextSparrow = random.Next(sparrowMinInterval, sparrowMaxInterval);

        (whipSource, whipClips) = loadClips(1, "SFX/birds/whippoorwill_");
        whipSource.volume = 0.23f;
        secondsTilNextWhip = random.Next(whipMinInterval, whipMaxInterval);

        (ambienceSource, ambienceClips) = loadClips(1, "SFX/ambience_fixloop_");

        ambienceSource.gameObject.name = "Ambience source";
        ambienceSource.volume = 0.2f;
        ambienceSource.clip = ambienceClips.Single();
        ambienceSource.loop = true;
        ambienceSource.Play();

        tick.Subscribe(_ => {
            playClipAtRandomInterval(robinSource, robinClips, ref secondsTilNextRobin, robinMinInterval, robinMaxInterval);
            playClipAtRandomInterval(finchSource, finchClips, ref secondsTilNextFinch, finchMinInterval, finchMaxInterval);
            playClipAtRandomInterval(sparrowSource, sparrowClips, ref secondsTilNextSparrow, sparrowMinInterval, sparrowMaxInterval);
            playClipAtRandomInterval(gooseSource, gooseClips, ref secondsTilNextGoose, gooseMinInterval, gooseMaxInterval);
            playClipAtRandomInterval(whipSource, whipClips, ref secondsTilNextWhip, whipMinInterval, whipMaxInterval);
        });

        datastore.turnCount.Subscribe(value => {
            if (value % 12 == 0 && value != 0) {
                pianoSource.clip = pianoClips.getRandomElement();
                pianoSource.Play();
            }
        });
    }

    void playClipAtRandomInterval(AudioSource source, List<AudioClip> clips, ref int secondsRemaining, int minInterval, int maxInterval) {
        if (secondsRemaining == 0) {
            source.clip = clips.getRandomElement();
            // swap to PlayWithEcho once that's working
            source.Play();
            secondsRemaining = random.Next(minInterval, maxInterval);
        } else {
            secondsRemaining--;
        }
    }

    (AudioSource, List<AudioClip>) loadClips(int numClips, string pathPrefix, string numSuffixLength="D2") {
        var clips = new List<AudioClip>();
        var source = new GameObject();
        source.transform.parent = this.transform;
        source.AddComponent<AudioSource>();
        for (var i = 1; i <= numClips; i++) {
            clips.Add(Resources.Load<AudioClip>($"{pathPrefix}{i.ToString(numSuffixLength)}"));
        }
        return (source.GetComponent<AudioSource>(), clips);
    }
}