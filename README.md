Respostas

1.Herança múltipla é situação de herdar as características, métodos e propriedades de várias classes, com isso você consegue chamar propriedades ou métodos de outras estruturas, o c# não suporta herança de várias classes, mas apenas de várias interfaces que são contratos base sem implementação.
O polimorfismo possibilita tratar o mesmo objeto de formas diferente, por exemplo, você pode criar uma classe abstrata com um método abstract, e fazer o override desse método em diferentes classes que herdam dessa classe abstract, exemplo de poliforfismo

public abstract class Animal {
    public abstract void MakeSound();
}

public class Dog : Animal {
    public override void MakeSound() {
        Console.WriteLine("Woof!");
    }
}

public class Cat : Animal {
    public override void MakeSound() {
        Console.WriteLine("Meow!");
    }
}

public class Program {
    public static void Main() {
        List<Animal> animals = new List<Animal> {
            new Dog(),
            new Cat()
        };foreach (var animal in animals) {
            animal.MakeSound();  // Saída: Woof! Meow!
        }
    }
}



2. O Solid são 5 princípios de programação que ajudam a escrever códigos mais organizados e fáceis de ler, algums exemplos são, o primeiro, Single responsability, que define que os objetos deve ter funcionalidades únicas e ter diversas funções na mesma estrutura, e o dependency inversion que utilizamos como injeção de dependência, evitando instanciar o objeto toda vez que queremos utiliza-los e a implementação se mantem nas abstrações 


3.O Entity framework é um ORM, sua funcionalidade é facilitar a comunicação com o banco de dados, ele consegue inclusive mapear as tabelas do BD como objetos no projeto C#, assim facilita o trabalho direto com os dados. Para melhorar a performance podemos utilizar a tecenica do lazy loading para obter apenas algumas quantidades de dados ao invés da tabela inteira, podemos fazer isso utilizando uma consulta através do Iqueryable objeto e utilizando as propriedades Skip, Take para fazer a paginação de forma correta e buscar na base apenas os dados daquela página


4.WebSockects são canais de comunicação bidirecional entre cliente servidor, essa comunicação se dá praticamente em tempo real e facilita o envio e recebimento de mensagens de chats ou que precisam ser em tempo real


5. A principal diferença entre microserviços  e monolíticos são que o segundo tem toda a estrutura do projeto em um único projeto, sendo assim, toda a implementação está dentro daquela solução e a publicação será de um único projeto. No caso dos microserviços teremos diversos projetos, cada um de acordo com o contexto definido de cada serviço, por exemplo, um produto ou um fornecedor, o negócio será dividido e único para cada microserviço e será um projeto para cada, além disso a publicação é separada. A principal vantagem e disponibilidade, se um dos microserviços cair ainda teremos o outro, claro se a infra do micro também for seguida, pois se publicarmos os microserviços em um mesmo servidos ou tivermos a mesma base de dados para todos os microserviços essa vantagem irá ser perdida. Em relação a utilização vai depender dos requisitos do projeto.



O projeto Blog foi feito em 4 camadas, a camada de view que também pode ser transformada em WebApi, a camada de domínio com as regras de negócio, a camada de repositório para comunicação com o banco de dados e a camanda de testes unitários onde fiz 1 teste de exemplo, os outros seguem o mesmo formato. Em relação ao login criei a página mas para facilitar fiz um login automatico no Identity server para mostrar a funcionalidade.
