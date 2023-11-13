using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    //Tweakable Properties
    [Header("Tweakable Properties")]
    public GameObject applePrefab;
    public Transform spawnPoint;
    public float speed=1f; 
    public float dx=8f;
    public float frequencyOfAppleDrops=2; //2 apples/sec
    public float probabilityOfDirChange=0.1f;
    float IntervalBetweenAppleDropsInSec;
    // Start is called before the first frame update
    void Start()
    {
        //Do Start things
        //Instantiate(applePrefab,this.transform
        IntervalBetweenAppleDropsInSec = 1 / frequencyOfAppleDrops;
        InvokeRepeating("DropApple", 2,IntervalBetweenAppleDropsInSec);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move AppleTree
        Vector3 pos = this.transform.position;
        pos.x += speed * Time.deltaTime; //
        this.transform.position = pos;

        //Check for and Change direction
        if(Mathf.Abs(pos.x)>dx || Random.value<probabilityOfDirChange)
        {
            speed = -speed;
        }
    }

    void DropApple()
    {
        // If you use Invoke only, you need to use both Instantiate and Invoke as below
        //Instantiate(applePrefab, transform.position,Quaternion.identity);
        //Invoke("DropApple", IntervalBetweenAppleDropsInSec);


        // If you use InvokeRepeating (once) you just need to use Instantiate here
        // Also, use a spawnPoint to specify where the apple is spawned 
        Instantiate(applePrefab, spawnPoint.position, Quaternion.identity);
        

    }
    /// <summary>
    /// Generate an approximate Standard Normally Distributed random variable 
    ///   by using "Count" Uniformly Distributed random variables 
    /// </summary>
    /// <param name="Count">The "count" of uniformly distributed random values to use; 
    /// the more, the close to normal distribution you get, 
    /// but also you are using more CPU, 
    /// so a tradoff is needed based on the problem at hand</param>
    /// <returns>An approximate Standard Normally Distributed random value </returns>
    float Normal01(int Count)
    {
        // We use RV=Random.value as Uniformly Distributed with:
        // average (RV)= 1/2, and
        // std(RV)=1/Sqrt(12)

        float r = 0;
        for(int i = 0; i < Count; i++)
        {
            r += Random.value;
        }
        r /= Count;  //r has std(r)=std(RV)/Sqrt(Count)  //See discussion below

        //modify r to preserve std(RV)
        r *= Mathf.Sqrt(Count); //std(r*Sqrt(Count))=Sqrt(Count)*std(r)=Sqrt(Count)*std(RV)/Sqrt(Count)=std(RV)=1/Sqrt(12)
        return r; 
    }

    /// <summary>
    /// Generate an approximate Normally Distributed random value 
    ///   with average mu and standard deviation sigma
    ///   by using "Count" Uniformly Distributed random variables 
    /// The theory behind using the histograms and a sum of several random variables:
    /// Ref: https://en.wikipedia.org/wiki/Law_of_large_numbers
    /// Section: Consequences
    /// </summary>
    /// <param name="Count">The "count" of uniformly distributed random values to use; 
    /// the more, the close to normal distribution you get, 
    /// but also you are using more CPU, 
    /// so a tradoff is needed based on the problem at hand.
    /// A more general way of doing this is, instead on Random.value, to use any random variable, 
    /// as long as you add i.i.d.-s (Independent and Identically Distributed).
    /// Here we assume the consecutive calls to Random.value are i.i.d.
    /// 
    /// Question: Can you devise a test to check if this is true or not (consecutive calls are i.i.d.)? 
    /// If yes, please run your test with both VS Random and UnityEngine Random classes.
    /// Please report your findings in class. Thanks.</param>
    /// 
    /// <param name="mu">The desired average of the newly created random variable</param>
    /// <param name="sigma">The desired standard deviation of the newly created random variable</param>
    /// <param name="Count">The number ("Count") of the Random.value-s to use</param>
    /// <returns>This generates an approximate Normally Distributed random value 
    /// with average mu and standard deviation sigma.</returns>
    float Normal(float mu, float sigma, int Count)
    {
     

        // LEGEND:
        // avg => Average  (sometimes writen as "mu")
        // std => Standard Deviation of the population (sometimes writen as "sigma")
        // Var => Variance (Var(X)= (std(X))^2) =E((X-avg(X))^2) (expected value of the square of diff from mean)
        // DEFINITIONS
        //------------
        // Let X be a discrete random variable with N values: x1,x2,...,xN
        // with probabilities p1, p2, ..., pN respectively
        // (f.e. uniform prob/s would be 1/N each).
        // Then by definition:
        //   avg(X)    =x1*p1+...+xN*pN=mu, and
        //   (std(X))^2=p1*(x1-mu)^2+...+pN*(xN-mu)^2, or
        //   std(X)    =Sqrt(p1*(x1-mu)^2+...+pN*(xN-mu)^2)
        //
        // For U01~U_[0,1] (see notations below), for all i, pi=1/N so the above definitions read:
        //    avg(U01)    =(x1+...+xN)/N, and
        //    (std(U01))^2=((x1-mu)^2+...+(xN-mu)^2)/N, or
        //    std(U01)    =Sqrt((x1-mu)^2+...+(xN-mu)^2)/Sqrt(N)
        //
        // NEW RANDOM VARIABLES FROM OLD
        // -----------------------------
        // With X then, for A and B two fixed arbitrary numbers,
        // we can create a new random variable Y which we "name" A+B*X, such that
        // whenever X takes the random value x with probability p(x), Y takes the random value y=A+B*x,
        // with the same probability p(x). There are some questions that can arise:
        //
        // Question 1: What is avg(Y)?
        // Answer 1: By definition:
        //   avg(Y)=y1*p1+...+yn*pn
        //         =(A+B*x1)*p1+...+(A+B*xn)*pn
        //         =A*(p1+...+pn)+B*(x1*p1+...+xn*pn)
        //         =A*1+B*avg(X)
        //         =A+B*avg(X)    (Q.E.D.)
        //
        // 
        // NB: This is also true for X a continuous random variable, however,
        // to see that you'd need some properties of integrals.
        // (For all intents and purposes you can think of integrals as sums of very small quantities.)
        //
        // Question 2: What is avg(Y)?
        // Answer 2: By definition:
        // (std(Y))^2=p1*(y1-avg(Y))^2+...+pN*(yN-avg(Y))^2
        //           =p1*((A+B*x1)-(A+B*avg(X)))^2+...+pN*((A+B*xN)-(A+B*avg(X)))^2
        //           =p1*(B*(x1-avg(X)))^2+...+pn*(B*(xN-avg(X)))^2
        //           =B^2*(p1*(x1-mu)^2+...+pN*(xN-mu)^2)
        //           =B^2*(std(X))^2, therefore,
        // std(Y)    =B*std(X)      (Q.E.D.)          
        //
        // Taken together the answers to the two above questions give us this result:
        // Result: Let X~X(mu, sigma^2), and A,B two arbitrary numbers.
        // Then for the random variable Y=A+B*X it is true the following:
        // 
        // Y~(A+B*mu,B^2*sigma^2)
        //  That is the Y is a random variable with:
        //      avg(Y)    =A+B*avg(X),  and
        //      (std(Y))^2=B^2*(std(X))^2=B^2*sigma^2
        //      std(Y)    =B*std(X)=B*sigma

        // GENERATING (APROXIMATE) NORMAL DISTRIBUTION FROM UNIFORM DISTRIBUTION
        //
        // Let X be a random variable with uniform distribution.
        // F.e. Random.value gives values in the [0,1) distributed uniformly.
        // That is if you get a big enough sample (say 10000 values) and
        //   divide [0,1) in K equal pieces (with width 1/K each), and 
        //   count how many of those values fall in any interval
        //   you would expect to see and aproximate number to 10000/K.
        //   
        //  If we create a new random variable
        //  Y=(X1+...+XP)/P, that is we average P values taken from X, we'd expect that:
        //  avg(Y)=avg(X) and (std(Y))^2=(std(X))^2/P or std(Y)=std(X)/Sqrt(P)
        //
        //  Also, the Law of Big Numbers (https://en.wikipedia.org/wiki/Law_of_large_numbers)
        //  tells us that when P becomes big, avg(Y) approaches avg(X) with arbitrary precision 
        // (std(Y) goes to 0, if std(X) is finite; there are some edge cases when this is not true though).
        //
        // NB: The fact that X has a normal distribution with mean mu and std(X)=sigma
        //     is denoted with X~N(mu,sigma^2)  (See f.e.: https://en.wikipedia.org/wiki/Normal_distribution) 
        // NB2: The fact that X has a uniform distribution in interval [a,b]
        //      is denoted with X~U_[a,b] (See f.e. https://en.wikipedia.org/wiki/Normal_distribution)

        if (Count <= 0)
        {            
            throw new System.ArgumentException("Count should be > 0!");              
        }
        float r = 0;
        for (int i = 0; i < Count; i++)
        {
            r += Random.value;
        }
        r /= Count; //this will need some adjustment to get it with the required sigma

        // We need to find A and B such that new_r=A+B*r has:
        // avg(new_r)=avg(A+B*r)=A+B*avg(r)=mu, and,
        // std(new_r)=B*std(r)=sigma
        //
        // know that:
        // r has avg(r) =1/2 => avg(A+B*r)  = A+B/2 = A, so avg(mu+B*r)= mu for any B
        // We need B such that std(r)=sigma
        //
        // In the following we use RV for Random.value.
        // We know that std(RV)=1/Sqrt(12)
        //
        // r has std(r)=std(RV)/Sqrt(Count) => std(mu+B*r)=B*std(r)=B*std(RV)/Sqrt(Count)
        // => Since we need std(r)=sigma
        // => sigma=B*std(RV)/Sqrt(Count)
        // => B=sigma/std(RV)*Sqrt(Count)
        // => B=sigma/(1/Sqrt(12))*Sqrt(Count)
        //     =sigma*Sqrt(12)*Sqrt(Count)
        //     =sigma*Sqrt(12*Count)    


        float B = sigma*Mathf.Sqrt(12f*Count);
        float new_r = mu + B * r;
        return new_r; 

    }
}
