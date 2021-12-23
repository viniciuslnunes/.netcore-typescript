const getSalutation = (): string => {
  let salutation = ''
  const currentHours = new Date().getHours()

  if (currentHours < 3) {
    salutation = 'Wow, olha a hora cara kkkk, o que vamos fazer agora?'
  } else if (currentHours < 6) {
    salutation = 'Cedo hoje hein? O que deseja por aqui?'
  } else if (currentHours < 12) {
    salutation = 'Bom dia! Em que posso ser útil?'
  } else if (currentHours < 18) {
    salutation = 'Boa tarde! Bora lá ver os usuários?'
  } else if (currentHours < 24) {
    salutation = 'Opa, ainda por aqui? O que precisas?'
  }

  return salutation
}

export default getSalutation
