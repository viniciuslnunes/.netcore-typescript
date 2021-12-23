import { SwitchTermPair } from '../entities/SwitchTermPair'

interface IValidateNextTermPairsValueParams {
  term: string;
  termType: 'old' | 'new';
  termPair: SwitchTermPair;
  currentTermPairs: SwitchTermPair[];
}

function validateNextTermPairsValue ({
  term,
  termType,
  termPair,
  currentTermPairs
}: IValidateNextTermPairsValueParams): SwitchTermPair[] {
  switch (termType) {
    case 'old':
      termPair.oldTerm = term
      break

    case 'new':
      termPair.newTerm = term
      break
  }

  let updatedTermPairs = currentTermPairs.map((pair) =>
    pair.id === termPair.id ? termPair : pair
  )

  updatedTermPairs = updatedTermPairs.filter(
    (pair) => pair.oldTerm || pair.newTerm
  )

  const thereIsAtLeastOnBlank = updatedTermPairs.some(
    (pair) => !pair.oldTerm && !pair.newTerm
  )

  if (!thereIsAtLeastOnBlank) {
    updatedTermPairs.push(new SwitchTermPair())
  }

  return updatedTermPairs
}

export { validateNextTermPairsValue }
