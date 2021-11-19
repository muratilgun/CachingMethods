## **Caching nedir ? Caching Çeşitleri nelerdir ?** 

- **In-Memory Caching**

- **Distributed Caching**

  

Çok sık kullandığımız dataları  ihtiyaç halinde  belli bir ortama kaydettikten sonra, bu datanın okunma işlemine **cache** adı verilir.

**Neden ihtiyaç duyarız??**

Websitesinden örnek verirsek, websitesinin daha hızlı olmasını sağlamak için, daha iyi bir kullanıcı deneyimi sağlamak için ve gereksiz ram maliyetini azalmak için çok sık kullandığımız dataları cacheleriz. Yani kısacası uygulamamızın performanısını ve ölçeklenebilirliğini arttıracaktır.

 Performanstan kasıt  örneğin ; websitesine 100 kişide girdiğinde aynı deneyimi sunmak 1000 kişi girincede aynı deneyimi sunmak.

_________________________________

## **In-Memory Caching nedir ?** 

Uygulamanın dataların uygulamayı barındıran web serverın memorysinde yani raminde tutulan datadır.

Ramden data okumak ile sabit diskten data okumak arasında hız farkı vardır. Ramde data okuma yazma işlemleri çok hızlı ilerler. Cachlemede de datalara hızlı bir şekilde erişebilmek için datalar cacheleme yapısında memoryde(ram) tutulur. 



<img src="https://i.ibb.co/C53ScBZ/cache-aside-diagram.png">

**1:** Öğenin şu anda önbellekte tutulup tutulmadığını belirlenir.
**2:** Öğe şu anda önbellekte değilse, öğe veri deposundan okunur.
**3:** Öğenin bir kopyası önbellekte saklanır.

Uygulamayı ayağa kaldırırken birden fazla instance kullanmıyorsak çok başarılı bir şekilde çalışır. Ama birden fazla ayağa kalkan uygulama instance var ise, en azından istikrarlı tutarlı bir şekilde kullanıcıya veri gösterebilmek için **Sticky Session** denilen yöntem ile load balancer  kullanılabilinir. Bu şekilde ilk istek hangi instance a yapılmışsa diğer isteklerde aynı servera yönlendirerek veri tutarlılığı sağlar. 



**Distributed Caching Nedir ?** 

<img src="https://i.ibb.co/nrvKfzL/Redis-Yaz-Serisi-3-Distributed-Caching-Nedir.png">



Cachelenecek olan dataların uygulamanın ayağa kalktığı host işletim sisteminin memorysinde değil tamamen ayrı bir cache servis içerisinde tutulma işlemine distributed caching deniyor. Bu ayrı bir servis içerisinde tutulması bize in-memory cache 'e göre bazı avantajlar sağlamaktadır.

- Veri tutarsızlığının önüne geçilir.
- eğer in memory cachede uygulamayı ayağa kaldıran host işletim sistemi bir restart gördüğü zaman bütün tutulan cacheler silinir. Ama distributed cache tarafında uygulama restart olsa dahi cachelenmiş data ayrı bir servis içerisinde tutulduğu için cache datalarını kaybetmemiş oluruz.
- In memory cachede tutulan data uygulamanın içerisinde olacağından dolayı çok daha hızlı bir şekilde ulaşım sağlanır. distributed  cachede ise dataların tutulduğu service'e tekrar istek yapmak zorunda kalınır. Bu yüzden in memory datadan daha yavaş bir şekilde cachede tutulan dataya erişilir. Dezavantaj olarak sunulacak bir konu olsa da çok yüksek aralıklarda bir hız farkı yoktur. Bu noktada veri tutarlılığı daha önemlidir. 

On-Demand ve PrePopulation Caching nedir ?

**On-Demand(Talep üzerine)**  datayı sadece talep olduğunda cachelemek yani istek geldiğinde ilk başta datayı sqlden alırız daha sonra cache kaydedilir. Ve ilgili requeste karşılık bir response dönülür.

**PrePopulation** uygulama ilk ayağa kalktığında bir data cachelenmek isteniyorsa, uygulama ayağa kaldırıldığında daha talep gelmeden, cachelenir. Çok sık kulanılan datalar için bu yöntem uygulanır.

Cache ömrü (Absolute time ve Sliding Time) nedir ?

Datayı cachelerken ne kadar süre cachede kalacağını belirliyebiliriz. Burada iki kavram vardır

Absolute time : cache için kesin ömür tarihi verir. Örneğin 5 dakika cache ömrü verilir ise 5 dakika sonra kesin olarak cache  memoryden gider.

Sliding Time : Bu yöntem ise cachelenmiş datanın memoryde ne kadar inactive kalacağı ile alakalıdır. Örneğin 5 dakika sliding time verilir ve 5 dakika içerisinde o dataya erişilirse artı olarak 5 dakika daha memoryide o data cachede tutulur. Bunun dezavantajı eski dataya herzaman ulaşılabilinir. Bu noktada cachelenmiş datalarda istemediğimiz bir döngü oluşabilinir. Bunu kırmak için sliding time veriyorken dataya absolute timeda belirleyebiliriz. Bu sayede cache ömrünü en fazla absolute timeda belirlediğimiz kadar uzatabiliriz. Bu aynı zamanda best praticedır.



**AddMemoryCache() servisi ve IMemoryCache** interface ile In-memory cache yapabilir

IMemoryCache TryGetValue() ve Remove() methodu değeri almak için kullanır/ memorydeki herhangi bir key'i silmek içi kullanılır

**GetOrCreate methodu**

**AbsoluteExpiration ve SlidingExpiration**

 **Cache Priority(öncelik) =**  Memory dolduğu zaman cachelenmiş datalardan hangisinin keyinin silineceğinin kurallarını  belirler

Her cache için NeverRemove seçer isek memory dolduğunda exception döner. 

**RegisterPostEvictionCallback method** =  MemoryCacheEntryOptions sınıfı üzerinde RegisterPostEvictionCallback methodu vardır bu sınıfın görevi

memoryden silinen bir data olduğu zaman neden silindiğini bildirir absolutetime dolduğu içinmi memoryde yer kalmadığı içinmi vs. 



as keywordü cast etmek için kullanılır. Eğer cast  edemez ise geriye null döner.

is keywordü cast edilip edilemeyeceğinin değerini döndürür. Eğer cast edilebilinirse, geriye true döndürür. cast edilemez ise false döndürür.

Redis Nedir ?

**R**emote **DI**ctionary **S**erver

Redis Veri Tipleri Nelerdir?

1. Redis String 

   - GET  
   - SET 
   - GETRANGE 
   - INCR 
   - INCRBY
   - APPEND

2. Redis List

   - LPUSH left push dizinin başına ekleme yapar 

   - RPUSH right push dizinin sonuna ekleme yapar
   - LPOP dizinin başında value siler
   - RPOP dizinin sonundan value siler
   - LRANGE let range 0 -1  sıfırıncı indexten başlayıp sonsuza kadar valueları sıralar
   - LINDEX index numarasına göre value getirir

3. Redis Set

   - SADD value eklemek için kullanılır.
   - SMEMBERS değeleri listelemek için kullanılır.
   - SREM value silmek için kullanılır

4. Redis Sorted Set

   - ZADD value eklemek için kullanılır.
   - valuelar uniqedir scorelar uniqe olmak zorunda değildir.
   - ZRANGE listelemek için kullanılır WITHSCORES ile birlikte kullanılıdığı zaman scorelarıda gösterir.
   - ZREM bir değer silinmek için kullanılır.

5. Redis hash

   -  HMSET value kaydetmek için kullanılır.
   - HGET value okumak için kullanılır
   - HDEL value silmek için kullanılır.
   - HGETALL  listelemek için kullanılır.

**Microsoft.Extensions.Caching.StackExchangeRedis**

- AddStackExchangeRedisCache
- IDistributedCache

StackExchange.Redis API



