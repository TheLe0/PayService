# PayService

DESAFIO - Processamento de Cobrança

O objetivo do desafio é avaliar a capacidade de construir um cenário de processamento de
cobrança baseada em micro serviços. Será levado em conta na avaliação os padrões de
projeto aplicados, a cobertura de testes e a performance da aplicação. A aplicação deve
possuir testes unitários, de integração e de performance.

API de clientes

Serviço que permite cadastro e consulta de clientes.O Método de cadastro deve
receber como parâmetros: Nome (string), Estado(string), CPF (string), deve validar o CPF e
formatá-lo para um valor numérico, além disso não devem ser aceitos CPFs duplicados ou
campos vazios. Persistir os dados da maneira que desejar. O Método de consulta
deve receber como parâmetros um CPF (string), validar o CPF e realizar a consulta.

API de cobranças

Serviço que registra uma cobrança para um determinado cliente. A API deve expor um
método que recebe como parâmetros a Data de vencimento, o CPF e o Valor da cobrança.
Após validar os dados, a API deve persistir as cobranças recebidas no banco de dados. A
API deve expor um método que recebe como parâmetro um CPF ou um mês de referência
e retorna as cobranças registradas de acordo com o filtro (CPF Vencimento e valor). Pelo
menos um dos filtros é obrigatório.

Serviço de cálculo de consumo

Serviço que calcula o valor das cobranças do cliente, seu processo consiste em consultar
todos os clientes cadastrados, calcular e registrar as cobranças da maneira mais rápida
possível (Usando as duas APIs construídas nos passos anteriores). O cálculo é feito da
seguinte maneira: 2 primeiros dígitos do CPF concatenados aos 2 últimos dígitos do
CPF do cliente. Por exemplo, no CPF 12345678, o valor cobrado será R$ 1278,00.

Bônus

Fazer um map/reduce das cobranças realizadas e gerar um relatório com o total cobrado no
mês para cada estado.

Pré-requisitos

● Todo código deve estar no Github do candidato e deve ser informado ao término.

● Aplicação deve conter todo o necessário para seu funcionamento, não deve ser
necessário instalar qualquer tipo de novo recurso externo, com exceção dos
frameworks e pacotes (nuguets);

● .Net;

● XUnit ou NUnit para testes;

● API RESTful;

● Persistência de livre escolha, como sugestão: Firebase, Redis Cloud, ou qualquer
serviço de nuvem, para evitar necessidades de instalação.

Dicas

● Surpreenda-nos e lembre-se menos é mais!
● Nomes são uma das coisas mais importantes!
● Documentação da API automática é um bônus muito bem-vindo!
● Lembre-se que o seu código é um espelho da sua qualidade!
● Desejamos que divirta-se e aproveite ao máximo esse momento de desafio!
